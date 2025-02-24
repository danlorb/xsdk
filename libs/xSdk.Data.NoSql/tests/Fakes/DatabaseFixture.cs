using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.IO;
using xSdk.Hosting;

namespace xSdk.Data.Fakes
{
    public class DatabaseFixture : TestHostFixture
    {
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
