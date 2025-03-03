using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using xSdk.Data;

namespace xSdk.Demos.Hosting
{
    public class MyDataHost(IDatalayerFactory factory, ILogger<MyDataHost> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Request informations from vault");

            var repo = factory.CreateRepository<IVaultRepository>();

            await repo.AddSecretAsync("mysecretkey", "mysecretvalue");
            var secret = await repo.GetSecretAsync();

            foreach (var kvp in secret)
                System.Console.WriteLine("Secret for Key '{0}' is '{1}'", kvp.Key, kvp.Value);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
