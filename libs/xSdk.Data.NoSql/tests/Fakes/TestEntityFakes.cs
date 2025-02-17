using Bogus;
using LiteDB;

namespace xSdk.Data.Fakes
{
    internal class TestEntityFakes : Fakes<TestEntity>
    {
        protected override void Build(Faker<TestEntity> builder)
        {
            builder
                .RuleFor(x => x.Id, f => ObjectId.NewObjectId())
                .RuleFor(x => x.Age, f => f.Random.Number(1, 100))
                .RuleFor(x => x.Name, f => f.Person.FullName);
        }
    }
}
