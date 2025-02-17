using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;

namespace xSdk.Extensions.Compression
{
    public class CompressionPluginTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void CreatePlugin()
        {
            var service = fixture
                .ConfigureServices(services => services.AddPluginServices())
                .ConfigurePlugin(builder => builder.EnableCompression())
                .GetRequiredService<IPluginService>();

            var plugin = service.GetPlugin<CompressionPlugin>();

            Assert.NotNull(plugin);
        }
    }
}
