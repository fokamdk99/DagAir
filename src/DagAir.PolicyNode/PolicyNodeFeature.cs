using DagAir.PolicyNode.MeasurementCommands;
using DagAir.PolicyNode.PolicyEvaluator;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode
{
    public static class PolicyNodeFeature
    {
        public static IServiceCollection AddPolicyNodeFeature(this IServiceCollection services)
        {
            services.AddScoped<IEvaluatePoliciesCommand, EvaluatePoliciesCommand>();
            services.AddScoped<IPolicyEvaluator, PolicyEvaluator.PolicyEvaluator>();

            return services;
        }
    }
}