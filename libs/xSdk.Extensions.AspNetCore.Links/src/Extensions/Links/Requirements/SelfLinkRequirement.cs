using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links.Requirements
{
    public class SelfLinkRequirement<TResource>
        : LinksHandler<SelfLinkRequirement<TResource>>,
            ILinksRequirement
    {
        public SelfLinkRequirement() { }

        public string Id { get; set; }

        protected override Task HandleRequirementAsync(
            LinksHandlerContext context,
            SelfLinkRequirement<TResource> requirement
        )
        {
            var route = context.CurrentRoute;
            var values = context.CurrentRouteValues;

            context.Links.Add(new LinkSpec(requirement.Id, route, values));
            context.Handled(requirement);
            return Task.FromResult(true);
        }
    }
}
