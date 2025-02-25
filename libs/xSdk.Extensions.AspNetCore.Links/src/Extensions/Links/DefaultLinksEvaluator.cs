using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace xSdk.Extensions.Links
{
    public class DefaultLinksEvaluator : ILinksEvaluator
    {
        private readonly LinksOptions options;
        private readonly ILinkTransformationContextFactory contextFactory;

        public DefaultLinksEvaluator(IOptions<LinksOptions> options, ILinkTransformationContextFactory contextFactory)
        {
            this.options = options.Value;
            this.contextFactory = contextFactory;
        }

        public void BuildLinks(IEnumerable<ILinkSpec> links, ILinkContainer container)
        {
            foreach (var link in links)
            {
                var context = contextFactory.CreateContext(link);
                try
                {
                    container.Add(
                        new Link()
                        {
                            Name = link.Id,
                            Href = options.HrefTransformation?.Transform(context),
                            Rel = options.RelTransformation?.Transform(context),
                            Method = link.HttpMethod.ToString(),
                        }
                    );
                }
                catch (Exception ex)
                {
                    throw new LinkTransformationException($"Unable to transform link {link.Id}", ex, context);
                }
            }
        }
    }
}
