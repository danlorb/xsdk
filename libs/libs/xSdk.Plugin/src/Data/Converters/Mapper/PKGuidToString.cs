using AutoMapper;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class PKGuidToString : IValueConverter<Guid, string>
    {
        public string Convert(Guid sourceMember, ResolutionContext context)
        {
            if (TryConvert(sourceMember, out string result))
            {
                return result;
            }
            return default;
        }

        internal static bool TryConvert(object value, out string converted)
        {
            converted = default;
            if (value != null && value is Guid guidValue)
            {
                converted = guidValue.ToString();
                return true;
            }
            return false;
        }
    }
}
