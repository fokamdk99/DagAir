using System.Reflection;
using DagAir.PolicyNode.Infrastructure;
using DagAir.PolicyNode.MeasurementCommands;
using DagAir.PolicyNode.PolicyEvaluator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode
{
    public static class PolicyNodeFeature
    {
        public static IServiceCollection AddPolicyNodeFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEvaluatePoliciesCommand, EvaluatePoliciesCommand>();
            services.AddScoped<IPolicyEvaluator, PolicyEvaluator.PolicyEvaluator>();

            return services;
        }
    }
}