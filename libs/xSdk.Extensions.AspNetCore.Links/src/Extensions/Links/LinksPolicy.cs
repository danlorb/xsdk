using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    public class LinksPolicy : ILinksPolicy
    {
        public LinksPolicy(IEnumerable<ILinksRequirement> requirements)
        {
            if (requirements == null)
                throw new ArgumentNullException(nameof(requirements));

            this.Requirements = new List<ILinksRequirement>(requirements).AsReadOnly();
        }

        public IReadOnlyList<ILinksRequirement> Requirements { get; }
    }
}
