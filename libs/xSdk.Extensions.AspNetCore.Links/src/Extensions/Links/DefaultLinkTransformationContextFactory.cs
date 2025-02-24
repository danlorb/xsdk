using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace xSdk.Extensions.Links
{
    public class DefaultLinkTransformationContextFactory : ILinkTransformationContextFactory
    {
        private readonly IActionContextAccessor actionAccessor;
        private readonly ILoggerFactory loggerFactory;
        private readonly LinkGenerator generator;

        public DefaultLinkTransformationContextFactory(
            IActionContextAccessor actionAccessor,
            ILoggerFactory loggerFactory,
            LinkGenerator generator
        )
        {
            this.actionAccessor = actionAccessor;
            this.loggerFactory = loggerFactory;
            this.generator = generator;
        }

        public LinkTransformationContext CreateContext(ILinkSpec spec)
        {
            return new LinkTransformationContext(spec, actionAccessor.ActionContext, generator);
        }
    }
}
