using System.Reflection;
using Microsoft.Extensions.Logging;
using Weikio.PluginFramework.Catalogs;
using xSdk.Extensions.IO;
using xSdk.Hosting;
using xSdk.Shared;

namespace xSdk.Extensions.Plugin
{
    internal partial class PluginService(IFileSystemService fsService, ILogger<PluginService> logger) : IPluginService
    {
        private List<PluginItem> plugins = new();

        public Task<TPlugin?> GetPluginAsync<TPlugin>(CancellationToken token = default) =>
            GetPluginsAsync<TPlugin>(token).ContinueWith(task => task.Result.FirstOrDefault());

        public async Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(CancellationToken token = default)
        {
            var searchResult = new List<PluginItem>();

            await LoadPluginsAsync();
            foreach (var item in plugins)
            {
                if (item.Plugin is TPlugin concretePlugin)
                {
                    searchResult.Add(item);
                }
            }

            return searchResult.OrderBy(x => x.Order).Select(x => x.Plugin).Cast<TPlugin>().ToList();
        }
    }
}
