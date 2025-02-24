using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public interface ILinksPolicyProvider
    {
        Task<ILinksPolicy> GetDefaultPolicyAsync();
        Task<ILinksPolicy> GetPolicyAsync<TResource>();
        Task<ILinksPolicy> GetPolicyAsync<TResource>(string policyName);
    }
}
