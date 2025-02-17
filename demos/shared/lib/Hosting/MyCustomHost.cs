using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace xSdk.Demos.Hosting
{
    public class MyCustomHost : IHostedService
    {
        private readonly ILogger<MyCustomHost> _logger;

        public MyCustomHost(ILogger<MyCustomHost> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();

            _logger.LogInformation("Host was started");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
