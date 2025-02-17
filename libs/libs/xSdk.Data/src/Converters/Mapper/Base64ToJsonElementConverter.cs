using System.Text.Json;
using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public class Base64ToJsonElementConverter : IValueConverter<string, JsonElement>
    {
        public JsonElement Convert(string sourceMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                var json = Base64Helper.ConvertFromBase64(sourceMember);
                return JsonDocument.Parse(json).RootElement;
            }

            return JsonDocument.Parse("{}").RootElement;
        }
    }
}
