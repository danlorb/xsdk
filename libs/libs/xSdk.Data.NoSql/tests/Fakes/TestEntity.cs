using System.ComponentModel.DataAnnotations;

namespace xSdk.Data.Fakes
{
    internal class TestEntity : NoSqlEntity
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
