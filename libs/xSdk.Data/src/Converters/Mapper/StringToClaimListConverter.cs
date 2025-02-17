using System.Security.Claims;
using AutoMapper;
using xSdk.Shared;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToClaimListConverter : IValueConverter<string, IEnumerable<Claim>>
    {
        public IEnumerable<Claim> Convert(string sourceMember, ResolutionContext context)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(sourceMember))
            {
                var converted = Base64Helper.ConvertFromBase64(sourceMember);
                claims = converted
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x =>
                    {
                        var item = x.Split("=", StringSplitOptions.RemoveEmptyEntries);
                        return new Claim(item[0].Trim(), item[1].Trim());
                    })
                    .ToList();
            }

            return claims;
        }
    }
}
