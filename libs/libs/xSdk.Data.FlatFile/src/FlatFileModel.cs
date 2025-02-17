using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data
{
    public abstract class FlatFileModel : Model, IModel<GuidStringPK, string>
    {
        public FlatFileModel()
        {
            this.PrimaryKey = new GuidStringPK();
        }

        public new string Id
        {
            get => PrimaryKey.GetValue<string>();
            set => PrimaryKey.SetValue(value);
        }
    }
}
