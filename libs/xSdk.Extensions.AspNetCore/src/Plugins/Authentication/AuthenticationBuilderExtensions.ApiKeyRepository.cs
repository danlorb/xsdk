using Microsoft.AspNetCore.Authentication;
using xSdk.Extensions.Authentication;

namespace xSdk.Plugins.Authentication
{
    public static partial class AuthenticationBuilderExtensions
    {
        private static IApiKeyRepository? ApiKeyRepository = new DefaultApiKeyRepository();

        public static AuthenticationBuilder AddApiKeyRepository<TRepository>(
            this AuthenticationBuilder builder
        )
            where TRepository : class, IApiKeyRepository, new()
        {
            ApiKeyRepository = new TRepository();
            return builder;
        }
    }
}
