using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Data.Mocks;

namespace xSdk.Data
{
    public class FakerTests
    {
        [Fact]
        public void CreateFakes()
        {
            var entity = FakeGenerator.Generate<TestEntityFakes, TestEntity>();

            Assert.NotNull(entity);
            Assert.True(entity.Id != Guid.Empty);
            Assert.NotNull(entity.Name);
            Assert.True(entity.Age > 0);
        }
    }
}
