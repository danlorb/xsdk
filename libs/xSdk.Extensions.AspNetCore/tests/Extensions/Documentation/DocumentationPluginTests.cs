using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using xSdk.Extensions.Documentation.Fakes;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Documentation;

namespace xSdk.Extensions.Documentation
{
    public class DocumentationPluginTests : IClassFixture<TestHostFixture>
    {
        private readonly IPluginService service;
        private readonly TestHostFixture fixture;

        public DocumentationPluginTests(TestHostFixture fixture)
        {
            fixture.EnablePlugin(builder => builder.EnableDocumentation<DocumentationPluginBuilderFake>());

            service = fixture.GetRequiredService<IPluginService>();

            this.fixture = fixture;
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

        [Fact]
        public void LoadSwaggerSchemaGenerator()
        {
            var schemaGenerator = this.fixture.Host.Services.GetRequiredService<ISchemaGenerator>();

            Assert.NotNull(schemaGenerator);
        }
    }
}
