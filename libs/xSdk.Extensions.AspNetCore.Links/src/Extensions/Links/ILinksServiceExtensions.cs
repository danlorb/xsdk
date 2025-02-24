using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public static class ILinksServiceExtensions
    {
        public static Task AddLinksAsync<T>(
            this ILinksService service,
            T linkContainer,
            ILinksPolicy /*<T>*/
            policy
        )
            where T : ILinkContainer
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));

            return service.AddLinksAsync(linkContainer, policy.Requirements);
        }

        public static Task AddLinksAsync<T>(
            this ILinksService service,
            T linkContainer,
            ILinksRequirement requirement
        )
            where T : ILinkContainer
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (requirement == null)
                throw new ArgumentNullException(nameof(requirement));

            return service.AddLinksAsync(linkContainer, new[] { requirement });
        }
    }
}
