using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Links
{
    public interface ILinksPluginBuilder : IPluginBuilder
    {
        void ConfigureLinks(LinksOptions options);
    }
}
