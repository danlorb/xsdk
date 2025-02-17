using AutoMapper;
using MongoDB.Bson;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class PKStringToObjectId : IValueConverter<string, ObjectId>
    {
        public ObjectId Convert(string sourceMember, ResolutionContext context)
        {
            if (TryConvert(sourceMember, out ObjectId result))
            {
                return result;
            }

            return default;
        }

        internal static bool TryConvert(object value, out ObjectId converted)
        {
            converted = default;
            if (value != null && value is string stringValue)
            {
                converted = ObjectId.Parse(stringValue.Trim());
                return true;
            }
            return false;
        }
    }
}
