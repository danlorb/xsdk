using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace xSdk.Extensions.Links
{
    public class LinkCondition<TResource>
    {
        public static readonly LinkCondition<TResource> None = new LinkCondition<TResource>(
            false,
            Enumerable.Empty<Func<TResource, bool>>(),
            Enumerable.Empty<IAuthorizationRequirement>(),
            Enumerable.Empty<string>()
        );

        public LinkCondition(
            bool requiresRouteAuthorization,
            IEnumerable<Func<TResource, bool>> assertions,
            IEnumerable<IAuthorizationRequirement> requirements,
            IEnumerable<string> policyNames
        )
        {
            this.RequiresRouteAuthorization = requiresRouteAuthorization;
            this.Assertions = new List<Func<TResource, bool>>(assertions).AsReadOnly();
            this.AuthorizationRequirements = new List<IAuthorizationRequirement>(
                requirements
            ).AsReadOnly();
            this.AuthorizationPolicyNames = new List<string>(policyNames).AsReadOnly();
        }

        public IReadOnlyList<Func<TResource, bool>> Assertions { get; }
        public IReadOnlyList<IAuthorizationRequirement> AuthorizationRequirements { get; }
        public IReadOnlyList<string> AuthorizationPolicyNames { get; }
        public bool RequiresRouteAuthorization { get; set; }
        public bool RequiresAuthorization =>
            RequiresRouteAuthorization
            || AuthorizationRequirements.Any()
            || AuthorizationPolicyNames.Any();
    }
}
