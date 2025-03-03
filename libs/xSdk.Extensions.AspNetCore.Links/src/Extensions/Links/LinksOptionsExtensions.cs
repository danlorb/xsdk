using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using xSdk.Data;

namespace xSdk.Extensions.Links
{
    public static class LinksOptionsExtensions
    {
        public static LinksOptions AddPolicy<TModel>(this LinksOptions options, Action<Policy<TModel>> configure)
            where TModel : IModel
        {
            var policy = new Policy<TModel>();
            configure?.Invoke(policy);

            options.Policies.Add(policy);

            return options;
        }
    }
}
