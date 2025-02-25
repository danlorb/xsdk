using xSdk.Hosting;

namespace xSdk.Extensions.Plugin
{
    public class PluginServiceTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void LoadPlugins()
        {
            var service = fixture.GetService<IPluginService>(services => services.AddPluginServices());

            var plugins = service.GetPlugins();
            Assert.NotNull(plugins);
            Assert.False(plugins.Any());
        }
    }
}
