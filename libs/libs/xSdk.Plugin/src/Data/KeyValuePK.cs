using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Data.Converters.Mapper;

namespace xSdk.Data
{
    public sealed class KeyValuePK : PrimaryKey<string>
    {
        private object syncObject = new();

        public KeyValuePK()
            : base(string.Empty) { }

        public KeyValuePK(string initialValue)
            : base(initialValue) { }

        protected override TType Convert<TType>(object value)
        {
            lock (syncObject)
            {
                return (TType)value;
            }

            return default;
        }
    }
}
