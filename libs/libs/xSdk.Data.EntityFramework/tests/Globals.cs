using xSdk.Data.Fakes;

namespace xSdk.Data
{
    internal static class Globals
    {
        internal const string DatabaseName = "UnitTestDb";
        internal const string DatalayerName = "UniqueTestName";

        internal static TestEntity[] Entities = new TestEntity[]
        {
            new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "Frodo",
                Age = 10,
            },
            new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "Sam",
                Age = 314,
            },
            new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "Gandalf",
                Age = 4876,
            },
        };
    }
}
