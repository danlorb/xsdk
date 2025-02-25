using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Demos.Hosting;
using xSdk.Extensions.IO;
using xSdk.Extensions.Variable;

const string APP_NAME = "host";
const string APP_COMPANY = "xdemos";
const string APP_PREFIX = "ho";

var host = xSdk
    .Hosting.Host.CreateBuilder(args, APP_NAME, APP_COMPANY, APP_PREFIX)
    .ConfigureServices(
        (context, services) =>
        {
            services
                .AddFileServices()
                .AddVariableServices(setup =>
                {
                    setup.AddEnvironmentVariablesWithoutSetup();
                })
                // Service um Informationen abzurufen
                // Ein eigener Host der benutzt werden soll
                .AddHostedService<MyCustomHost>();
        }
    )
    .Build();

var logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting {AppName}", APP_NAME);

await host.RunAsync();
