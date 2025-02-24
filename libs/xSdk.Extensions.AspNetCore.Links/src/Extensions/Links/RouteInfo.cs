using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace xSdk.Extensions.Links
{
    public class RouteInfo
    {
        public RouteInfo(string name, HttpMethod httpMethod, IControllerMethodInfo methodInfo)
        {
            this.RouteName =
                name
                ?? $"{methodInfo?.ControllerType.Namespace}.{methodInfo?.ControllerType?.Name}.{methodInfo?.MethodName}";
            this.HttpMethod = httpMethod;
            this.MethodInfo = methodInfo;
        }

        public string RouteName { get; }
        public HttpMethod HttpMethod { get; }
        public IControllerMethodInfo MethodInfo { get; }
        public Type ControllerType => MethodInfo?.ControllerType;
        public string MethodName => MethodInfo?.MethodName;
        public Type ReturnType => MethodInfo.ReturnType;
        public string ControllerName => ControllerType?.Name?.Replace("Controller", String.Empty);
        public IEnumerable<LinksAttribute> LinksAttributes =>
            MethodInfo?.GetAttributes<LinksAttribute>();
        public IEnumerable<AuthorizeAttribute> AuthorizeAttributes =>
            MethodInfo?.GetAttributes<AuthorizeAttribute>();
    }
}
