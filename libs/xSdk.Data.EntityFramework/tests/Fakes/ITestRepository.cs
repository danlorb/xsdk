using xSdk.Data;
using xSdk.Data.Fakes;

namespace xSdk.Data.Fakes
{
    internal interface ITestRepository : IRepository
    {
        Task AddDataAsync(TestEntity[] samples, CancellationToken token = default);

        Task<IEnumerable<TestEntity>> GetDataAsync(CancellationToken token = default);
    }
}
