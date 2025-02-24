using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace xSdk.Extensions.Links
{
    public class DefaultLinksPolicyProvider : ILinksPolicyProvider
    {
        private readonly LinksOptions options;

        public DefaultLinksPolicyProvider(IOptions<LinksOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            this.options = options.Value;
        }

        public Task<ILinksPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(options.DefaultPolicy);
        }

        public Task<ILinksPolicy> GetPolicyAsync<TResource>()
        {
            return Task.FromResult(options.GetPolicy<TResource>());
        }

        public Task<ILinksPolicy> GetPolicyAsync<TResource>(string policyName)
        {
            return Task.FromResult(options.GetPolicy<TResource>(policyName));
        }
    }
}
