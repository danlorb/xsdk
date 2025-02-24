using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Plugin
{
    public class PluginDescription : IPlugin
    {
        public string? Name { get; internal set; }

        public Version? Version { get; internal set; }

        public bool IsEnabled { get; internal set; }
    }
}
