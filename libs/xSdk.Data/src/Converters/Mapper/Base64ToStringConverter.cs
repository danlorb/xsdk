using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public class Base64ToStringConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return Base64Helper.ConvertFromBase64(sourceMember);
            }
            return string.Empty;
        }
    }
}
