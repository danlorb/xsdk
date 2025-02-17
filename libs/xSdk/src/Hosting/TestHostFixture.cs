using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public class TestHostFixture : IDisposable
    {
        private IHostBuilder builder;
        private IHost? host;

        private List<Action<IServiceCollection>> configureServicesDelegates = new();
        private List<
            Action<HostBuilderContext, IServiceCollection>
        > configureServicesWithContextDelegates = new();
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

        public IPluginService PluginSystem =>
            Build().Services.GetService<IPluginService>()
            ?? throw new SdkException("Plugin Services was not added");

        public string AppName => SlimHost.Instance.AppName;

        public string AppCompany => SlimHost.Instance.AppCompany;

        public string AppPrefix => SlimHost.Instance.AppPrefix;

        public string AppVersion => SlimHost.Instance.AppVersion;

        public TService? GetService<TService>()
            where TService : notnull => Host.Services.GetRequiredService<TService>();

        public TService? GetService<TService>(Action<IServiceCollection> configure)
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetService<TService>();

        public IEnumerable<TService> GetServices<TService>()
            where TService : notnull => Host.Services.GetServices<TService>();

        public IEnumerable<TService> GetServices<TService>(Action<IServiceCollection> configure)
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetServices<TService>();

        public TService GetRequiredService<TService>()
            where TService : notnull => Host.Services.GetRequiredService<TService>();

        public TService GetRequiredService<TService>(Action<IServiceCollection> configure)
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetRequiredService<TService>();

        public TService? GetKeyedService<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetKeyedService<TService>(serviceKey);

        public TService? GetKeyedService<TService>(
            object? serviceKey,
            Action<IServiceCollection> configure
        )
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetKeyedService<TService>(serviceKey);

        public IEnumerable<TService> GetKeyedServices<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetKeyedServices<TService>(serviceKey);

        public IEnumerable<TService> GetKeyedServices<TService>(
            object? serviceKey,
            Action<IServiceCollection> configure
        )
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetKeyedServices<TService>(serviceKey);

        public TService? GetRequiredKeyedService<TService>(object? serviceKey)
            where TService : notnull => Host.Services.GetRequiredKeyedService<TService>(serviceKey);

        public TService? GetRequiredKeyedService<TService>(
            object? serviceKey,
            Action<IServiceCollection> configure
        )
            where TService : notnull =>
            TestHost
                .CreateBuilder()
                .ConfigureServices(configure)
                .Build()
                .Services.GetRequiredKeyedService<TService>(serviceKey);

        public TestHostFixture Invoke<TPlugin>()
        {
            return this;
        }

        public TestHostFixture ConfigureServices(Action<IServiceCollection> configure)
        {
            configureServicesDelegates.Add(configure);

            return this;
        }

        public TestHostFixture ConfigureServices(
            Action<HostBuilderContext, IServiceCollection> configure
        )
        {
            configureServicesWithContextDelegates.Add(configure);

            return this;
        }

        public TestHostFixture ConfigurePlugin(Action<IHostBuilder> configure)
        {
            builderDelegates.Add(configure);
            return this;
        }

        private IHost Build()
        {
            if (host == null)
            {
                builder.ConfigureServices(
                    (context, services) =>
                    {
                        foreach (var configure in configureServicesDelegates)
                        {
                            configure?.Invoke(services);
                        }

                        foreach (var configure in configureServicesWithContextDelegates)
                        {
                            configure?.Invoke(context, services);
                        }
                    }
                );

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
    }
}
