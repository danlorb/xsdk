using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Demos.Configs;
using xSdk.Extensions.IO;
using xSdk.Extensions.Variable;
using xSdk.Hosting;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

const string APP_NAME = "webapi";
const string APP_COMPANY = "xdemos";
const string APP_PREFIX = "webapi";

var host = xSdk.Hosting.Host
    .CreateBuilder(args, APP_NAME, APP_COMPANY, APP_PREFIX)
    .ConfigureServices((context, services) =>
    {
        services
            .AddFileServices()
            .AddVariableServices();
    })
    .ConfigurePlugin<DocumentationConfig>()
    .ConfigurePlugin<AuthenticationConfig>()
    .EnableWebApi()
    .EnableDocumentation()
    // .EnableAuthentication()    
    .EnableWebSecurity()
    .EnableLinks()
    .Build();

var logger = LogManager.GetCurrentClassLogger();
logger.Info("Starting {AppName}", APP_NAME);

await host.RunAsync();
