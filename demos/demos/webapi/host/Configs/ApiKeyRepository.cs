using AspNetCore.Authentication.ApiKey;
using System.Collections.ObjectModel;
using System.Security.Claims;
using xSdk.Extensions.Authentication;
using xSdk.Security.Claims;

namespace xSdk.Demos.Configs
{
    internal class ApiKeyRepository : IApiKeyRepository
    {
        public Task<IApiKey?> GetApiKeyAsync(string key)
        {
            return Task.FromResult(ApiKeys.SingleOrDefault(x => string.Compare(x.Key, key) == 0));
        }

        // List of ApiKeys only for Demo Purposes. Its highly recommended to host this in a external service
        private List<IApiKey> ApiKeys = new List<IApiKey>
        {
            new ApiKey
            {
                Key = "338879db-73e4-4f50-93fa-45f70d35ac15",
                OwnerName = "Only Read user",
                Claims = new ReadOnlyCollection<Claim>(new[]
                {
                    ClaimCreator.CreateClaim(MyClaimTypes.MyTableA.Permission, MyClaimValues.Permissions.Read)
                })
            },
            new ApiKey
            {
                Key = "47cf5874-4c73-45ae-82dd-f7f2e66b79c6",
                OwnerName = "Write User",
                Claims = new ReadOnlyCollection<Claim>(new[]
                {
                    ClaimCreator.CreateClaim(MyClaimTypes.MyTableA.Permission, MyClaimValues.Permissions.Read),
                    ClaimCreator.CreateClaim(MyClaimTypes.MyTableA.Permission, MyClaimValues.Permissions.Write)
                })
            },
        };
    }
}
