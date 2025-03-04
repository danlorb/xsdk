using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Hosting;

namespace xSdk.Data.Mocks
{
    public class DatabaseFixture : TestHostFixture
    {
        public DatabaseFixture()
        {
            // Init Unit Tests
            ConfigureServices(services =>
            {
                services
                    // Add DbContext Factory
                    .AddDbContextFactory<TestDbContext>(options =>
                    {
                        // Use InMemory Database
                        options.UseInMemoryDatabase(Globals.DatabaseName).ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    })
                    .AddDatalayer(builder =>
                    {
                        builder.UseEntityFramework<TestDbContext>(Globals.DatalayerName).MapRepository<ITestRepository, TestRepository>(Globals.DatalayerName);
                    });
            });

            Factory = Host.Services.GetRequiredService<IDatalayerFactory>();
        }

        internal IDatalayerFactory Factory { get; init; }
    }
}
