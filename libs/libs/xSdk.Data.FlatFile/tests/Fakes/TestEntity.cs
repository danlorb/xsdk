using System.ComponentModel.DataAnnotations;

namespace xSdk.Data.Fakes
{
    internal class TestEntity : FlatFileEntity
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
