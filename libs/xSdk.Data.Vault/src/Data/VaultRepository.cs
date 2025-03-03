using NLog;
using System.Text.Json;
using System.Text.Json.Nodes;
using VaultSharp;

namespace xSdk.Data
{
    public partial class VaultRepository : Repository, IVaultRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddSecretAsync(string mountpoint, string path, Dictionary<string, object> data, CancellationToken token = default)
        {
            try
            {
                var client = Database.Open<VaultClient>();                
                var result = await client.V1.Secrets.KeyValue.V2.WriteSecretAsync(path, data, mountPoint: mountpoint);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "A Error occured while Vault will readed");
                throw;
            }

            return true;
        }

        public async Task<IDictionary<string, object?>> GetSecretAsync(string mountPoint, string path, CancellationToken token = default)
        {
            try
            {
                var client = Database.Open<VaultClient>();

                var secret = await client.V1.Secrets.KeyValue.V2.ReadSecretAsync(path.ToLower(), mountPoint: mountPoint);
                if (secret != null)
                {
                    return secret.Data.Data.ToDictionary(k => k.Key, v =>
                    {
                         if (v.Value is JsonElement element)
                         {
                             return element.GetString() as object;
                         }
                         return v.Value as object;
                     });
                }
                else
                    throw new SdkException($"No Secrets found in Vault '{path}'");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "A Error occured while Vault will readed");
                throw;
            }
        }
    }
}
