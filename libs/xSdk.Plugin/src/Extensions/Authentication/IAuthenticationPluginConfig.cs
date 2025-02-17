using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Authentication
{
    public interface IAuthenticationPluginConfig : IPlugin
    {
        void ConfigureAuthentication(AuthenticationBuilder builder);

        void ConfigureAuthorization(AuthorizationOptions options);

        void TryRetrieveAuthenticationScheme(HttpContext context, out string? scheme);
    }
}
