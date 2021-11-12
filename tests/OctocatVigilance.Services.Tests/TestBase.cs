using Microsoft.Extensions.Logging;
using Moq;

namespace OctocatVigilance.Services.Tests
{
    public abstract class TestBase<T>
    {
        protected TestBase()
        {
            _logger = new Mock<ILogger<T>>().Object;
        }

        public ILogger<T> _logger { get; }
    }
}