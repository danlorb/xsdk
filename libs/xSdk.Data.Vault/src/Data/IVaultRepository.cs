using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xSdk.Data
{
    public interface IVaultRepository : IRepository
    {
        private const string DefaultMountPoint = "secret";
        private const string DefaultPath = "default";

        Task<bool> AddSecretAsync(string key, object data, CancellationToken token = default)
            => AddSecretAsync(DefaultMountPoint, DefaultPath, key, data, token);

        Task<bool> AddSecretAsync(string path, string key, object data, CancellationToken token = default)
            => AddSecretAsync(DefaultMountPoint, path, key, data, token);

        Task<bool> AddSecretAsync(string mountpoint, string path, string key, object data, CancellationToken token = default)
            => AddSecretAsync(mountpoint, path, new Dictionary<string, object> { { key, data } }, token);

        Task<bool> AddSecretAsync(Dictionary<string, object> data, CancellationToken token = default)
            => AddSecretAsync(DefaultMountPoint, DefaultPath, data, token);

        Task<bool> AddSecretAsync(string path, Dictionary<string, object> data, CancellationToken token = default)
            => AddSecretAsync(DefaultMountPoint, path, data, token);

        Task<bool> AddSecretAsync(string mountpoint, string path, Dictionary<string, object> data, CancellationToken token = default);

        Task<IDictionary<string, object?>> GetSecretAsync(CancellationToken token = default)
            => GetSecretAsync(DefaultMountPoint, DefaultPath, token);

        Task<IDictionary<string, object?>> GetSecretAsync(string mountPoint, string path, CancellationToken token = default);
    }
}
