using AspNetCore.Authentication.ApiKey;

namespace xSdk.Extensions.Authentication
{
    public sealed class DefaultApiKeyRepository : IApiKeyRepository
    {
        public Task<IApiKey> GetApiKeyAsync(string key)
        {
            // Default dynamic ApiKey
            return Task.FromResult<IApiKey>(new ApiKey { Key = Guid.NewGuid().ToString(), OwnerName = "Dynamic API Key User" });
        }
    }
}
