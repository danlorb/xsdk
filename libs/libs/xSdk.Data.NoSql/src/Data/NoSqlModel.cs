using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data
{
    public abstract class NoSqlModel : Model, IModel<NoSqlModelPK, string>
    {
        public NoSqlModel()
        {
            this.PrimaryKey = new NoSqlModelPK();
        }

        public new string Id
        {
            get => PrimaryKey.GetValue<string>();
            set => PrimaryKey.SetValue(value);
        }
    }
}
