using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Data.Fakes;

namespace xSdk.Data
{
    public class FakerTests
    {
        [Fact]
        public void CreateFakes()
        {
            var entity = FakeGenerator.Generate<TestEntityFakes, TestEntity>();

            Assert.NotNull(entity);
            Assert.NotNull(entity.Id);
            Assert.NotNull(entity.Name);
            Assert.True(entity.Age > 0);
        }
    }
}
