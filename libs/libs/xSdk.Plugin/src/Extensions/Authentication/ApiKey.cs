using System.Security.Claims;
using AspNetCore.Authentication.ApiKey;
using xSdk.Data;

namespace xSdk.Extensions.Authentication
{
    public class ApiKey : Model, IApiKey
    {
        private IReadOnlyCollection<Claim> _claims;

        public ApiKey()
        {
            _claims = new List<Claim>();
        }

        public string Key { get; set; }

        public string OwnerName { get; set; }

        public IReadOnlyCollection<Claim> Claims { get; set; }
    }
}
