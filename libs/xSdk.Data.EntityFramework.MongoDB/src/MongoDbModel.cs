using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data
{
    public class MongoDbModel : Model, IModel<MongoDbModelPK, string>
    {
        public MongoDbModel()
        {
            this.PrimaryKey = new MongoDbModelPK();
        }

        public new string Id
        {
            get => PrimaryKey.GetValue<string>();
            set => PrimaryKey.SetValue(value);
        }
    }
}
