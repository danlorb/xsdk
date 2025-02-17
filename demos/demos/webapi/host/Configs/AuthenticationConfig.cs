using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using xSdk.Extensions.Authentication;
using xSdk.Extensions.Plugin;

namespace xSdk.Demos.Configs
{
    internal class AuthenticationConfig : PluginBase, IAuthenticationPluginConfig
    {
        // Global Constants for an easier handling
        public const string Policy_OnlyRead = "OnlyRead";
        public const string Policy_ReadAndWrite = "ReadAndWrite";

        public void ConfigureAuthentication(AuthenticationBuilder builder)
        {
            // builder.AddApiKeyRepository<ApiKeyRepository>();            
        }

        public void ConfigureAuthorization(AuthorizationOptions options)
        {
            // Configure here your Policies which will used in your Controller
            options.AddPolicy(Policy_ReadAndWrite, policy =>
            {
                //policy.RequireClaim(MyClaimTypes.MyTableA.Permission, MyClaimValues.Permissions.Write);
                policy.RequireRole("Admin");
            });

            options.AddPolicy(Policy_OnlyRead, policy =>
            {
                // policy.RequireClaim(MyClaimTypes.MyTableA.Permission, MyClaimValues.Permissions.Read);
                policy.RequireRole("User");
            });
        }

        public void TryRetrieveAuthenticationScheme(HttpContext context, out string? scheme)
        {
            scheme = null;
        }
    }
}
