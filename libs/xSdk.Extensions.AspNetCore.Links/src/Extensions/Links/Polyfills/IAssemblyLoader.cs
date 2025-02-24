using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links.Polyfills
{
    public interface IAssemblyLoader
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
