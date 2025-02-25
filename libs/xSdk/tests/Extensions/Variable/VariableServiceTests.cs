using xSdk.Extensions.Variable.Fakes;
using xSdk.Hosting;

namespace xSdk.Extensions.Variable
{
    public class VariableServiceTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void LoadServiceWithVariables()
        {
            var service = fixture.GetService<IVariableService>(services => services.AddVariableServices());

            service.RegisterSetup<EnvironmentSetup>();

            Assert.NotNull(service);
            Assert.NotEmpty(service.Variables);
        }

        [Fact]
        public void LoadEnvironemtVariablesWithNoValidationErrors()
        {
            var service = fixture.GetService<IVariableService>(services => services.AddVariableServices());

            Assert.NotNull(service);
            service.RegisterSetup<SetupWithNoPrefix>();

            Assert.NotEmpty(service.Variables);
            var setup = service.GetSetup<SetupWithNoPrefix>(false);
            Assert.NotNull(setup);
        }
    }
}
