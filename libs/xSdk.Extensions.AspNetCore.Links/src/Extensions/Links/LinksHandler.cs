using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public abstract class LinksHandler<TRequirement> : ILinksHandler
        where TRequirement : ILinksRequirement
    {
        public async Task HandleAsync(LinksHandlerContext context)
        {
            foreach (var req in context.Requirements.OfType<TRequirement>())
            {
                await HandleRequirementAsync(context, req);
            }
        }

        protected abstract Task HandleRequirementAsync(
            LinksHandlerContext context,
            TRequirement requirement
        );
    }
}
