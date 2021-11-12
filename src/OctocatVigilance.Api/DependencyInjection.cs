using OctocatVigilance.Api.GitHub;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OctocatVigilance.Api
{
    public static class DependencyInjection
    {
        public static void AddGithubApi(this IServiceCollection services)
        {
            services.TryAddSingleton<IGitHubApiClientFactory, GitHubApiClientFactory>();
        }
    }
}
