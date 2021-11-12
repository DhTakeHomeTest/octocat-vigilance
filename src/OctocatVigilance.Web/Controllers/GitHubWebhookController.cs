using System;
using System.Threading;
using System.Threading.Tasks;
using OctocatVigilance.Services.Processors;
using GithubWebhook.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OctocatVigilance.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubWebhookController : ControllerBase
    {
        private readonly ILogger<GitHubWebhookController> _logger;
        private readonly IPushEventProcessor _pep;

        public GitHubWebhookController(ILogger<GitHubWebhookController> logger, IPushEventProcessor pep)
        {
            _logger = logger;
            _pep = pep;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(PushEvent pe, CancellationToken token)
        {
            var addedProtectionToMain = await _pep.ProcessPushEventAsync(pe, token);
            return addedProtectionToMain ? (IActionResult) Ok() : NoContent();
        }
    }
}
