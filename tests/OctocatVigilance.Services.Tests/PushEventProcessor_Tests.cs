using System.Threading;
using OctocatVigilance.Api.GitHub;
using OctocatVigilance.Services.Processors;
using GithubWebhook.Events;
using Moq;
using Octokit;
using Xunit;

namespace OctocatVigilance.Services.Tests
{
    public class PushEventProcessor_Tests : TestBase<PushEventProcessor>
    {
        [Fact]
        public async void ReturnsFalseIfNotMaster()
        {
            var ghcf = new Mock<IGitHubApiClientFactory>();
            
            var sut = new PushEventProcessor(_logger, ghcf.Object);
            var repo = new GithubWebhook.Common.Repository { Id = 1 };
            var pe = new PushEvent { Ref = "refs/heads/otherbranch", Created = true, Repository = repo };

            var result = await sut.ProcessPushEventAsync(pe, It.IsAny<CancellationToken>());

            Assert.False(result);
        }

        [Fact]
        public async void ReturnsFalseIfNotCreated()
        {
            var ghcf = new Mock<IGitHubApiClientFactory>();
            var sut = new PushEventProcessor(_logger, ghcf.Object);
            var repo = new GithubWebhook.Common.Repository { Id = 2 };
            var pe = new PushEvent { Ref = "refs/heads/main", Created = false, Repository = repo };

            var result = await sut.ProcessPushEventAsync(pe, It.IsAny<CancellationToken>());

            Assert.False(result);
        }

        [Fact]
        public async void WhenMasterBranchCreated_ProtectMainBranch_AddIssue_ReturnTrue()
        {
            var repo = new GithubWebhook.Common.Repository { Id = 3 };
            var pe = new PushEvent { Ref = "refs/heads/main", Created = true, Repository = repo };
            var issue = new Issue();
            var username = "MyUser";

            var rbc = new Mock<IRepositoryBranchesClient>();
            var ic = new Mock<IIssuesClient>();
            ic.Setup(x => x.Create(repo.Id.Value, It.IsAny<NewIssue>())).ReturnsAsync(issue);

            var ghcf = new Mock<IGitHubApiClientFactory>();
            ghcf.Setup(x => x.GetRepositoryBranchesClient()).Returns(rbc.Object);
            ghcf.Setup(x => x.GetIssuesClient()).Returns(ic.Object);
            ghcf.Setup(x => x.GetGitHubApiUsername()).Returns(username);

            var sut = new PushEventProcessor(_logger, ghcf.Object);
            
            var result = await sut.ProcessPushEventAsync(pe, It.IsAny<CancellationToken>());

            Assert.True(result);
            rbc.Verify(x => x.UpdateBranchProtection(repo.Id.Value, "main", It.IsAny<BranchProtectionSettingsUpdate>()));
            ic.Verify(x => x.Create(repo.Id.Value, It.IsAny<NewIssue>()));
            ic.Verify(x => x.Update(repo.Id.Value, issue.Number, It.Is<IssueUpdate>(y=>y.Body.Contains($"@{username}"))));
        }
    }
}
