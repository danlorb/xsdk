using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    internal static class MethodAnalyzer
    {
        internal static List<MethodDescription> Analyze(MethodBase? caller)
        {
            var classType = caller.ReflectedType;
            var methods = classType.GetMethods();

            var descriptions = new List<MethodDescription>();
            foreach (var method in methods)
            {
                var httpAttribute = method.GetCustomAttribute<HttpMethodAttribute>();
                if (httpAttribute != null && httpAttribute.HttpMethods.Any() && !string.IsNullOrEmpty(httpAttribute.Name))
                {
                    var description = new MethodDescription();
                    description.Action = method;
                    description.ControllerType = classType;

                    description.HttpMethod = HttpMethod.Parse(httpAttribute.HttpMethods.First());
                    description.MethodName = httpAttribute.Name;
                    description.RouteTemplate = httpAttribute.Template;

                    var linksAttribute = method.GetCustomAttribute<LinksAttribute>();
                    if (linksAttribute != null)
                    {
                        description.PolicyName = linksAttribute.PolicyName;
                    }

                    var authorizeAttribute = method.GetCustomAttribute<AuthorizeAttribute>();
                    if(authorizeAttribute!= null)
                    {
                        if (!string.IsNullOrEmpty(authorizeAttribute.Roles))
                        {
                            description.AuthRoles = authorizeAttribute.Roles.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        }

                        description.AuthPolicy = authorizeAttribute.Policy;
                    }

                    descriptions.Add(description);
                }
            }

            return descriptions;
        }
    }
}
