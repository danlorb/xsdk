using xSdk.Data.Fakes;

namespace xSdk.Data
{
    public class EntityTests
    {
        [Fact]
        public void EntityShouldCreated()
        {
            var entity = new TestEntity { Age = 42, Name = "John Doe" };

            Assert.NotNull(entity);
            Assert.IsType<Guid>(entity.PrimaryKey.GetValue());
        }

        [Fact]
        public void EntityShouldCreatedWithAutomaticGeneratedPK()
        {
            var entity = new TestEntity { Age = 42, Name = "John Doe" };

            Assert.NotNull(entity);
            Assert.NotNull(entity.Id);
            Assert.IsType<Guid>(entity.PrimaryKey.GetValue());
        }
    }
}
