using AspNetCore.Authentication.ApiKey;

namespace xSdk.Extensions.Authentication
{
    internal static class AuthenticationDefaults
    {
        internal const string DefaultScheme = "NotConfigured";

        internal static class ApiKeyAuth
        {
            internal const string Name = "API Key Authentication";

            internal static class InHeader
            {
                internal const string Header = "X-API-KEY";
                internal const string Scheme =
                    $"{ApiKeyDefaults.AuthenticationScheme}InHeaderScheme";
            }

            internal static class InAuthorizationHeader
            {
                internal const string Header = "ApiKey";
                internal const string Scheme =
                    $"{ApiKeyDefaults.AuthenticationScheme}InAuthorizationHeaderScheme";
            }
        }

        internal static class MulitAuth
        {
            internal const string Scheme = "MultiAuthScheme";
        }
    }
}
