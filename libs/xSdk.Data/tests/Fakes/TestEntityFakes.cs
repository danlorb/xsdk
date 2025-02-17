using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace xSdk.Data.Fakes
{
    internal class TestEntityFakes : Fakes<TestEntity>
    {
        protected override void Build(Faker<TestEntity> builder)
        {
            builder
                .RuleFor(x => x.Name, f => f.Person.FirstName)
                .RuleFor(x => x.Age, f => f.Random.Number(1, 100));
        }
    }
}
