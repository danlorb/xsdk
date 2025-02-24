using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public interface IControllerMethodInfo
    {
        Type ControllerType { get; }
        Type ReturnType { get; }
        string MethodName { get; }
        IEnumerable<TAttribute> GetAttributes<TAttribute>()
            where TAttribute : Attribute;
    }
}
