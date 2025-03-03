using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public interface IPolicy
    {
        List<RoutedLink> Links { get; }
    }
}
