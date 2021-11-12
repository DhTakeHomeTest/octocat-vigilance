using System;
using System.Threading;
using System.Threading.Tasks;
using OctocatVigilance.Api.GitHub;
using GithubWebhook.Events;
using Microsoft.Extensions.Logging;
using Octokit;

namespace OctocatVigilance.Services.Processors
{
    public interface IPushEventProcessor
    {
        Task<bool> ProcessPushEventAsync(PushEvent pe, CancellationToken token);
    }

    public class PushEventProcessor : IPushEventProcessor
    {
        private readonly ILogger<PushEventProcessor> _logger;
        private readonly IGitHubApiClientFactory _clientFactory;

        public PushEventProcessor(ILogger<PushEventProcessor> logger, IGitHubApiClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        
        public async Task<bool> ProcessPushEventAsync(PushEvent pe, CancellationToken token)
        {
            try
            {
                if (pe.Created == true && pe.Ref == "refs/heads/main" && pe.Repository.Id.HasValue)
                {
                    var branchClient = _clientFactory.GetRepositoryBranchesClient();
                    var issueClient = _clientFactory.GetIssuesClient();

                    await branchClient.UpdateBranchProtection(pe.Repository.Id.Value, "main", new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(false, true, 1)));
                    var issue = await issueClient.Create(pe.Repository.Id.Value, new NewIssue("Branch protection added") { });
                    issue = await issueClient.Update(pe.Repository.Id.Value, issue.Number, new IssueUpdate { Body = $"@{_clientFactory.GetGitHubApiUsername()} has added new branch protection:\n-Require a pull request before merging\n -Require approvals\n -Require review from Code Owners" });
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            return false;
        }
    }
}