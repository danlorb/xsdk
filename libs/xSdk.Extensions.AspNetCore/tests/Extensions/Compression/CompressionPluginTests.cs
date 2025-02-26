using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Compression;

namespace xSdk.Extensions.Compression
{
    public class CompressionPluginTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void CreatePlugin()
        {
            fixture.Builder.EnableCompression().ConfigureServices((context, services) => services.AddPluginServices());

            var service = fixture.Host.Services.GetRequiredService<IPluginService>();
            var plugin = service.GetPlugin<CompressionPlugin>();

            Assert.NotNull(plugin);
        }
    }
}
