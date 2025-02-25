using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;

namespace xSdk.Plugins.Documentation
{
    public interface IDocumentationPluginBuilder : IPluginBuilder
    {
        void ConfigureSwagger(SwaggerGenOptions options);

        void ConfigureSwaggerUi(SwaggerUIOptions options);

        void ConfigureApiDescriptions(Dictionary<string, OpenApiInfo> descriptions);
    }
}
