using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public class StringToBase64Converter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return Base64Helper.ConvertToBase64(sourceMember);
            }

            return string.Empty;
        }
    }
}
