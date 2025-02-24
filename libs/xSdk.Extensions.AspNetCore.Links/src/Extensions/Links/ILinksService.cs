using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public interface ILinksService
    {
        Task AddLinksAsync<TResource>(TResource linkContainer)
            where TResource : ILinkContainer;
        Task AddLinksAsync<TResource>(
            TResource linkContainer,
            IEnumerable<ILinksRequirement> requirements
        )
            where TResource : ILinkContainer;
        Task AddLinksAsync<TResource>(TResource linkContainer, string policyName)
            where TResource : ILinkContainer;
    }
}
