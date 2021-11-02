using DagAir.Components.HttpClients;
using DagAir.PolicyNode.Infrastructure;
using DagAir.PolicyNode.Integrations;
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
            services.AddDagAirHttpClientsFeature();
            services.AddPoliciesIntegrationsFeature();

            return services;
        }
    }
}