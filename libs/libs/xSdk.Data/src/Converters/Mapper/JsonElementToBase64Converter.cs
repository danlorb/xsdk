using System.Text.Json;
using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public class JsonElementToBase64Converter : IValueConverter<JsonElement, string>
    {
        public string Convert(JsonElement sourceMember, ResolutionContext context)
        {
            var json = sourceMember.GetRawText();
            return Base64Helper.ConvertToBase64(json);
        }
    }
}
