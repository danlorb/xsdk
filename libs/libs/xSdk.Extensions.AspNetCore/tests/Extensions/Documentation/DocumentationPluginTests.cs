using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using xSdk.Extensions.Documentation.Fakes;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;

namespace xSdk.Extensions.Documentation
{
    public class DocumentationPluginTests : IClassFixture<TestHostFixture>
    {
        private IPluginService service;

        public DocumentationPluginTests(TestHostFixture fixture)
        {
            this.service = fixture
                .ConfigureServices(services => services.AddPluginServices())
                .ConfigurePlugin(builder =>
                {
                    builder.AddPlugin<DocumentationPluginFake>().EnableDocumentation();
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
            var plugins = service.GetPlugins<IDocumentationPluginConfig>();

            Assert.NotNull(plugins);
            Assert.Equal(1, plugins.Count());
        }

        [Fact]
        public void InvokePluginConfiguration()
        {
            var services = new ServiceCollection();
            service.Invoke<IServicesPluginConfig>(x => x.ConfigureServices(services));

            Assert.NotNull(services);
            Assert.True(services.Count() > 0);
        }
    }
}
