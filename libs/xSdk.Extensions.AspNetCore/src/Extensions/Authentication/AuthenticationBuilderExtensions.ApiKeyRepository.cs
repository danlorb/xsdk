using Microsoft.AspNetCore.Authentication;
using xSdk.Hosting;
using xSdk.Plugins;

namespace xSdk.Extensions.Authentication
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
