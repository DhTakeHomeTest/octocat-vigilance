using OctocatVigilance.Services.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OctocatVigilance.Services
{
    public static class DependencyInjection
    {
        public static void AddProcessorServices(this IServiceCollection services)
        {
            services.TryAddScoped<IPushEventProcessor, PushEventProcessor>();
        }
    }
}
