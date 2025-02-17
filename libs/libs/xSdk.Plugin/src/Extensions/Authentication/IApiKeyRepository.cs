using AspNetCore.Authentication.ApiKey;

namespace xSdk.Extensions.Authentication
{
    public interface IApiKeyRepository
    {
        Task<IApiKey?> GetApiKeyAsync(string key);
    }
}
