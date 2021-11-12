using System;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace OctocatVigilance.Api.GitHub
{
    public interface IGitHubApiClientFactory
    {
        IIssuesClient GetIssuesClient();
        IRepositoryBranchesClient GetRepositoryBranchesClient();
        string GetGitHubApiUsername();
    }

    public class GitHubApiClientFactory : IGitHubApiClientFactory
    {
        private readonly IGitHubClient client;
        private readonly string githubApiUser;

        public GitHubApiClientFactory(IConfiguration configuration)
        {
            githubApiUser = configuration["GithubClientSettings:User"];
            var githubApiPat = configuration["GithubClientSettings:Pat"];
            var basicAuth = new Credentials(githubApiUser, githubApiPat);
            client = new GitHubClient(new ProductHeaderValue(githubApiUser)) { Credentials = basicAuth };
        }

        public IRepositoryBranchesClient GetRepositoryBranchesClient()
        {
            return client.Repository.Branch;
        }

        public IIssuesClient GetIssuesClient()
        {
            return client.Issue;
        }

        public string GetGitHubApiUsername()
        {
            return githubApiUser;
        }
    }
}
