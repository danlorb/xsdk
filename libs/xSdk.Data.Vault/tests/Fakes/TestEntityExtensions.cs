using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data.Fakes
{
    internal static class TestEntityExtensions
    {
        internal static KeyValuePair<string, object> ConverToDictionary(this TestEntity entity)
        {
            return new KeyValuePair<string, object>(entity.Key, entity.Value);
        }
    }
}
