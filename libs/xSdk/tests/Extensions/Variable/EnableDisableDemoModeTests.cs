using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Hosting;

namespace xSdk.Extensions.Variable
{
    public class EnableDisableDemoModeTests(TestHostFixture fixture) : IClassFixture<TestHostFixture>
    {
        [Fact]
        public void EnableDemoMode()
        {
            fixture.EnableDemoMode();

            var service = fixture.GetService<IVariableService>();
            var setup = service.GetSetup<EnvironmentSetup>();

            Assert.True(setup.IsDemo);
        }

        [Fact]
        public void DisableDemoMode()
        {
            fixture.DisableDemoMode();

            var service = fixture.GetService<IVariableService>();
            var setup = service.GetSetup<EnvironmentSetup>();

            Assert.False(setup.IsDemo);
        }
    }
}
