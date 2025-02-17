using System.Security.Claims;
using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class ClaimListToStringConverter : IValueConverter<IEnumerable<Claim>, string>
    {
        public string Convert(IEnumerable<Claim> sourceMember, ResolutionContext context)
        {
            string result = default;

            if (sourceMember != null)
            {
                result = sourceMember
                    .Select(x => $"{x.Type}={x.Value}")
                    .Aggregate((a, b) => a + ", " + b);
            }

            return Base64Helper.ConvertToBase64(result);
        }
    }
}
