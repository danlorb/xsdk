using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using xSdk.Data;

namespace xSdk.Demos.Data
{
    [Table("sample")]
    public class SampleEntity : MongoDbEntity, ISampleEntity
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
