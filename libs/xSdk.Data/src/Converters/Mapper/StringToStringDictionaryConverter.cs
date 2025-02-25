using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToStringDictionaryConverter : IValueConverter<string, Dictionary<string, string>>
    {
        public Dictionary<string, string> Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                var tmp = Base64Helper.ConvertFromBase64(sourceMember);
                var items = tmp.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x =>
                    {
                        var items = x.Split("=", StringSplitOptions.RemoveEmptyEntries);

                        if (items.Count() == 2)
                            return new KeyValuePair<string, string>(items[0].Trim(), items[1].Trim());
                        else
                            return new KeyValuePair<string, string>(items[0].Trim(), "");
                    });

                return new Dictionary<string, string>(items);
            }

            return new Dictionary<string, string>();
        }
    }
}
