using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Extensions.Commands;
using xSdk.Extensions.IO;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public class TestHostFixture : IDisposable
    {
        private IHostBuilder builder;
        private IHost? host;

        private List<Action<IServiceCollection>> servicesDelegates = new();
        private List<Action<HostBuilderContext, IServiceCollection>> hostServicesDelegates = new();
        private List<Action<WebHostBuilderContext, IServiceCollection>> webhostServicesDelegates = new();

        private List<Action<IHostBuilder>> builderDelegates = new();

        private bool disposed;

        public TestHostFixture()
        {
            builder = TestHost.CreateBuilder();
        }

        ~TestHostFixture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IHostBuilder Builder => builder;

        public IHost Host => Build();

        public string AppName => SlimHostInternal.Instance.AppName;

        public string AppCompany => SlimHostInternal.Instance.AppCompany;

        public string AppPrefix => SlimHostInternal.Instance.AppPrefix;

        public string AppVersion => SlimHostInternal.Instance.AppVersion;

        public TService? GetService<TService>()
            where TService : notnull => Host.Services.GetRequiredService<TService>();

        public IEnumerable<TService> GetServices<TService>()
            where TService : notnull => Host.Services.GetServices<TService>();

        public TService GetRequiredService<TService>()
            where TService : notnull => Host.Services.GetRequiredService<TService>();

        public TService? GetKeyedService<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetKeyedService<TService>(serviceKey);

        public IEnumerable<TService> GetKeyedServices<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetKeyedServices<TService>(serviceKey);

        public TService? GetRequiredKeyedService<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetRequiredKeyedService<TService>(serviceKey);

        public TService? GetService<TService>(Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetService<TService>();

        public IEnumerable<TService> GetServices<TService>(Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetServices<TService>();

        public TService GetRequiredService<TService>(Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetRequiredService<TService>();

        public TService? GetKeyedService<TService>(object? serviceKey, Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetKeyedService<TService>(serviceKey);

        public IEnumerable<TService> GetKeyedServices<TService>(object? serviceKey, Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetKeyedServices<TService>(serviceKey);

        public TService? GetRequiredKeyedService<TService>(object? serviceKey, Action<IServiceCollection> configure)
            where TService : notnull => TestHost.CreateBuilder().ConfigureServices(configure).Build().Services.GetRequiredKeyedService<TService>(serviceKey);

        public TestHostFixture ConfigureServices(Action<IServiceCollection> configure)
        {
            servicesDelegates.Add(configure);

            return this;
        }

        public TestHostFixture ConfigureHostServices(Action<HostBuilderContext, IServiceCollection> configure)
        {
            hostServicesDelegates.Add(configure);

            return this;
        }

        public TestHostFixture ConfigureWebHostServices(Action<WebHostBuilderContext, IServiceCollection> configure)
        {
            webhostServicesDelegates.Add(configure);

            return this;
        }

        public TestHostFixture EnablePlugin(Action<IHostBuilder> configure)
        {
            builderDelegates.Add(configure);
            return this;
        }

        private IHost Build()
        {
            if (host == null)
            {
                builder
                    .ConfigureServices(
                        (context, services) =>
                        {
                            foreach (var configure in servicesDelegates)
                            {
                                configure?.Invoke(services);
                            }

                            foreach (var configure in hostServicesDelegates)
                            {
                                configure?.Invoke(context, services);
                            }
                        }
                    )
                    .ConfigureWebHost(webhostBuilder =>
                    {
                        webhostBuilder.ConfigureServices(
                            (context, services) =>
                            {
                                foreach (var configure in webhostServicesDelegates)
                                {
                                    configure?.Invoke(context, services);
                                }
                            }
                        );
                    });

                foreach (var configure in builderDelegates)
                {
                    configure?.Invoke(builder);
                }

                host = builder.Build();
            }
            return host;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                RestoreDemoMode();

                // Dispose managed state (managed objects).
                LogManager.Flush();
                LogManager.Shutdown();

                host?.StopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                host?.Dispose();
            }

            // Free unmanaged resources.
            disposed = true;
        }

        protected string GetEnvironmentVariable(string key)
        {
            var imageName = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(imageName))
            {
                throw new SdkException($"The environment variable '{key}' is not defined.");
            }

            return imageName;
        }

        private bool? currentDemoMode;

        public void EnableDemoMode()
        {
            var setup = GetService<IVariableService>().GetSetup<EnvironmentSetup>();
            if (!currentDemoMode.HasValue)
            {
                currentDemoMode = setup.IsDemo;
            }
            setup.IsDemo = true;
        }

        public void DisableDemoMode()
        {
            var setup = GetService<IVariableService>().GetSetup<EnvironmentSetup>();
            if (!currentDemoMode.HasValue)
            {
                currentDemoMode = setup.IsDemo;
            }
            setup.IsDemo = false;
        }

        private void RestoreDemoMode()
        {
            if (currentDemoMode.HasValue)
            {
                var setup = GetService<IVariableService>().GetSetup<EnvironmentSetup>();
                setup.IsDemo = currentDemoMode.Value;
            }
        }
    }
}
