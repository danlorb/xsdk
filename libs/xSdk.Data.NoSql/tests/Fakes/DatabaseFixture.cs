using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.IO;
using xSdk.Hosting;

namespace xSdk.Data.Fakes
{
    public class DatabaseFixture : TestHostFixture
    {
        private IContainer? container = null;

        public DatabaseFixture()
        {
            // Init Unit Tests
            ConfigureServices(services =>
            {
                services.AddDatalayer(builder =>
                {
                    var currentFolder = Path.Combine(FileSystemHelper.GetExecutingFolder(), "data", Guid.NewGuid().ToString("N"));
                    if (!Directory.Exists(currentFolder))
                    {
                        Directory.CreateDirectory(currentFolder);
                    }

                    var imageName = GetEnvironmentVariable("GENERIC_LINUX_IMAGE_NAME");
                    container = new ContainerBuilder()
                        .WithImage(imageName)
                        .WithPortBinding(8080, true)
                        .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
                        .WithAutoRemove(true)
                        .WithBindMount(currentFolder, "/data/db")
                        .WithImagePullPolicy(PullPolicy.Missing)
                        .Build();

                    container.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                    builder
                        // Enable FlatFile
                        .UseNoSql(
                            Globals.DatalayerName,
                            config =>
                            {
                                config.Path = currentFolder;
                                config.FileName = $"{Globals.DatabaseName}.store";
                            }
                        )
                        .MapRepository<ITestRepository, TestRepository>(Globals.DatalayerName);
                });
            });

            Factory = Host.Services.GetRequiredService<IDatalayerFactory>();
        }

        internal IDatalayerFactory Factory { get; init; }
    }
}
