using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Data;
using xSdk.Demos.Data;
using xSdk.Demos.Hosting;

const string APP_NAME = "datalayer-entityframework";
const string APP_COMPANY = "xdemos";
const string APP_PREFIX = "de";

var host = xSdk.Hosting.Host
    .CreateBuilder(args, APP_NAME, APP_COMPANY, APP_PREFIX)
    .ConfigureServices((context, services) =>
    {
        services
            // Add DbContext Factory
            .AddDbContextFactory<SampleDbContext>(options =>
            {
                // Use InMemory Database
                options
                    .UseInMemoryDatabase("MySampleDatabase")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            })

            // Sample for Entity Datalayer
            .AddDatalayer(builder =>
            {
                builder
                    // Enable Entityframework
                    .UseEntityFramework<SampleDbContext>("MySampleDatalayer", config =>
                    {
                        config.TransactionsEnabled = false;
                    })

                    // Add Repositories to the Layer
                    .MapRepository<ISampleRepository, SampleRepository>();
            })
            .AddHostedService<MyDataHost>();
    })
    .Build();

var logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting {AppName}", APP_NAME);

await host.RunAsync();

