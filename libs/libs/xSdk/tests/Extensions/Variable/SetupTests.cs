using xSdk.Hosting;

namespace xSdk.Extensions.Variable
{
    public class SetupTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void LoadSetup()
        {
            var service = fixture.GetService<IVariableService>(services =>
                services.AddVariableServices()
            );
            var setup = service.GetSetup<EnvironmentSetup>();

            Assert.NotNull(setup);
        }

        [Fact]
        public void LoadSetupFromInterface()
        {
            var service = fixture.GetService<IVariableService>(services =>
                services.AddVariableServices()
            );
            var setup = service.GetSetup<IEnvironmentSetup>();

            Assert.NotNull(setup);
        }

        [Fact]
        public void LoadSetupWithoutSlimHost()
        {
            var setup = new EnvironmentSetup();

            setup.AppCompany = "MyCompany";

            Assert.NotNull(setup);
            Assert.Equal("MyCompany", setup.AppCompany);
        }
    }
}
