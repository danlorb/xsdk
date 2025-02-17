using xSdk.Data;
using xSdk.Data.Fakes;

namespace xSdk.Data.Fakes
{
    internal class TestRepository
        : EntityFrameworkRepository<TestDbContext, TestEntity>,
            ITestRepository
    {
        public Task AddDataAsync(TestEntity[] samples, CancellationToken token = default)
        {
            return this.InsertAsync(samples, token);
        }

        public Task<IEnumerable<TestEntity>> GetDataAsync(CancellationToken token = default)
        {
            return this.SelectListAsync(token);
        }
    }
}
