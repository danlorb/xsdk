using Microsoft.Extensions.Options;
using xSdk.Data.Mocks;

namespace xSdk.Data
{
    public class InsertDataTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task InsertDataWithDefaultMountPointAndPath()
        {
            var factory = fixture.GetService<IDatalayerFactory>();
            var repo = factory.CreateRepository<IVaultRepository>();

            var fake = FakeGenerator.Generate<TestEntityFakes, TestEntity>();            
            await repo.AddSecretAsync(fake.Key, fake.Value);

            var data = await repo.GetSecretAsync();
            var entity = data.FirstOrDefault();

            Assert.Equal(fake.Key, entity.Key);
            Assert.Equal(fake.Value, entity.Value);
        }
    }
}
