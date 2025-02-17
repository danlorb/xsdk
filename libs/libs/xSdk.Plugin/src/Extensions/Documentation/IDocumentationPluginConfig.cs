using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Documentation
{
    public interface IDocumentationPluginConfig : IPlugin
    {
        void ConfigureSwagger(SwaggerGenOptions options);

        void ConfigureSwaggerUi(SwaggerUIOptions options);

        void ConfigureApiDescriptions(OpenApiInfo info);
    }
}
