using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace xSdk.Extensions.Links
{
    public class DefaultLinksHandlerContextFactory : ILinksHandlerContextFactory
    {
        private readonly IRouteMap routeMap;
        private readonly ILinkAuthorizationService authService;
        private readonly ActionContext actionContext;
        private readonly ILoggerFactory loggerFactory;

        public DefaultLinksHandlerContextFactory(
            IRouteMap routeMap,
            ILinkAuthorizationService authService,
            IActionContextAccessor actionAccessor,
            ILoggerFactory loggerFactory
        )
        {
            this.routeMap = routeMap;
            this.authService = authService;
            this.actionContext = actionAccessor.ActionContext;
            this.loggerFactory = loggerFactory;
        }

        public LinksHandlerContext CreateContext(IEnumerable<ILinksRequirement> requirements, object resource)
        {
            var logger = loggerFactory.CreateLogger<LinksHandlerContext>();
            return new LinksHandlerContext(requirements, routeMap, authService, logger, actionContext, resource);
        }
    }
}
