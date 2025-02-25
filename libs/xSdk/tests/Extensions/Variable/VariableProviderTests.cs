using xSdk.Extensions.Variable.Fakes;
using xSdk.Hosting;

namespace xSdk.Extensions.Variable
{
    public class VariableProviderTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void RegisterVariableProvider()
        {
            var service = fixture.GetService<IVariableService>(services => services.AddVariableServices());

            var ex = Record.Exception(() => service.RegisterProvider(typeof(TestVariableProvider)));

            Assert.Null(ex);
        }
    }
}
