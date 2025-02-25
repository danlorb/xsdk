using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringDictionaryToStringConverter : IValueConverter<Dictionary<string, string>, string>
    {
        public string Convert(Dictionary<string, string> sourceMember, ResolutionContext context)
        {
            string result = default;

            if (sourceMember != null && sourceMember.Any())
            {
                result = sourceMember.Select(x => $"{x.Key}={x.Value}").Aggregate((a, b) => a + ";" + b);

                result = Base64Helper.ConvertToBase64(result);
            }

            return result;
        }
    }
}
