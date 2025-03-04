using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Hosting;

namespace xSdk.Data.Mocks
{
    public class DatabaseFixture : TestHostFixture
    {
        private IContainer? container = null;

        public DatabaseFixture()
        {
            // Init Unit Tests
            ConfigureServices(services =>
            {
                services
                    .AddDatalayer(builder =>
                    {
                        var imageName = Environment.GetEnvironmentVariable("VAULT_IMAGE_NAME");
                        if (string.IsNullOrEmpty(imageName))
                        {
                            throw new SdkException("The environment variable VAULT_IMAGE_NAME is not defined.");
                        }

                        var container = new ContainerBuilder()
                            .WithImage(imageName)
                            .WithPortBinding(8200, true)
                            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(8200))
                            .WithImagePullPolicy(PullPolicy.Always)
                            .Build();

                        container.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        var port = container.GetMappedPublicPort(8200);
                        var (stdout, stderr) = container.GetLogsAsync(timestampsEnabled: false).GetAwaiter().GetResult();

                        var splitted = stdout.Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        var rootToken = splitted.Where(x => x.IndexOf("Root Token:") > -1).FirstOrDefault()?.Replace("Root Token:", "").Trim();
                        var unsealKey = splitted.Where(x => x.IndexOf("Unseal Key:") > -1).FirstOrDefault()?.Replace("Unseal Key:", "").Trim();

                        builder
                            // Enable Vault
                            .UseVault(
                                Globals.DatalayerName,
                                _ =>
                                {
                                    _.Host = $"http://localhost:{port}";
                                    _.TokenAuth = new() { Token = rootToken };
                                }
                            );
                    });
            });

            Factory = Host.Services.GetRequiredService<IDatalayerFactory>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    container?.StopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }
                catch
                {
                    // Nothing to tell
                }
            }
            base.Dispose(disposing);
        }

        internal IDatalayerFactory Factory { get; init; }
    }
}
