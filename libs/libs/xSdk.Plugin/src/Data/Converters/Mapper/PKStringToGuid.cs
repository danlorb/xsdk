using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class PKStringToGuid : IValueConverter<string, Guid>
    {
        public Guid Convert(string sourceMember, ResolutionContext context)
        {
            if (TryConvert(sourceMember, out Guid result))
            {
                return result;
            }

            return default;
        }

        internal static bool TryConvert(object value, out Guid converted)
        {
            converted = default;
            if (value != null && value is string stringValue)
            {
                converted = Guid.Parse(stringValue);
                return true;
            }
            return false;
        }
    }
}
