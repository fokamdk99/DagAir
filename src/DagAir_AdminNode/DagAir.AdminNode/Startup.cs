using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.AdminNode.Hubs;
using DagAir.AdminNode.Infrastructure.Swagger;
using DagAir.Components.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace DagAir.AdminNode
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddControllers();
            services.AddSignalR();
            services.AddDagAirHealthChecks(new List<string>());
            services.AddCors();
            services.AddConfiguredSwagger();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseConfiguredSwagger();

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseCors(builder =>
            {
                builder.AllowCredentials();
                var origin = configuration.GetSection("serviceUrls:webAdminApp").Value; 
                builder.WithOrigins(origin.Substring(0, origin.Length-1));
                builder.AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chatHub");
                endpoints.MapHealthCheckEndpoints();
            });
            
            var notificationHub = app.ApplicationServices.GetRequiredService<IHubContext<ChatHub, IAdminNodeHub>>();
            SubscribeToRedisChannelAndBroadcaast(notificationHub);
        }
        
        private void SubscribeToRedisChannelAndBroadcaast(IHubContext<ChatHub, IAdminNodeHub> notificationHub)
        {
            var config = new ConfigurationOptions()
            {
                KeepAlive = 0,
                AllowAdmin = true,
                EndPoints = { { "redis", 6379 },{ "localhost", 6379 }, { "host.docker.internal", 6379 } },
                ConnectTimeout = 5000,
                ConnectRetry = 5,
                SyncTimeout = 5000,
                AbortOnConnectFail = false,
            };
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);

            ISubscriber sub = redis.GetSubscriber();

            sub.Subscribe("Notification-Channel", async (channel, info) =>
            {
                IDatabase db = redis.GetDatabase();
                var message = db.ListRightPop("Notification-List");

                if (message != RedisValue.Null)
                {
                    await notificationHub.Clients.All.PoliciesEvaluationResultEvent(message);
                    Console.WriteLine($"Forwarded message: { message }");
                }
            });
        }
    }
}