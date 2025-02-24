using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Documentation.Fakes;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Documentation;

namespace xSdk.Extensions.Documentation
{
    public class DocumentationPluginTests : IClassFixture<TestHostFixture>
    {
        private readonly IPluginService service;

        public DocumentationPluginTests(TestHostFixture fixture)
        {
            this.service = fixture
                .ConfigureServices(services => services.AddPluginServices())
                .ConfigurePlugin(builder =>
                {
                    builder.EnableDocumentation<DocumentationPluginBuilderFake>();
                })
                .GetRequiredService<IPluginService>();
        }

        [Fact]
        public void CreatePlugin()
        {
            var plugin = service.GetPlugin<DocumentationPlugin>();

            Assert.NotNull(plugin);
        }

        [Fact]
        public void GetPluginConfigurations()
        {
            var plugins = service.GetPlugins<IDocumentationPluginBuilder>();

            Assert.NotNull(plugins);
            Assert.Single(plugins);
        }

        [Fact]
        public void InvokePluginConfiguration()
        {
            var services = new ServiceCollection();
            service.Invoke<WebHostPluginBase>(x => x.ConfigureServices(null, services));

            Assert.NotNull(services);
            Assert.True(services.Count > 0);
        }
    }
}
