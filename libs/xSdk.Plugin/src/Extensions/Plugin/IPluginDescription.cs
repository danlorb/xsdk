using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Plugin
{
    public interface IPluginDescription
    {
        string? Name { get; }

        Version? Version { get; }

        string? Description { get; }

        string? ProductVersion { get; }

        string? Tag { get; }

        List<string> Tags { get; }
    }
}
