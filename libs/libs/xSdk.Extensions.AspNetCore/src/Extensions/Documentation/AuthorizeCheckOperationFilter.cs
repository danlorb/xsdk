using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using xSdk.Extensions.Authentication;
using xSdk.Hosting;
using xSdk.Plugins;

namespace xSdk.Extensions.Documentation
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
                context
                    .MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>()
                    .Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add(
                    "401",
                    new OpenApiResponse { Description = "Unauthorized" }
                );
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                var requirements = new List<OpenApiSecurityRequirement>();
                requirements.Add(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme()
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = AuthenticationDefaults.ApiKeyAuth.Name,
                                    Type = ReferenceType.SecurityScheme,
                                },
                            },
                            new List<string>()
                        },
                    }
                );

                operation.Security = requirements;
            }
        }
    }
}
