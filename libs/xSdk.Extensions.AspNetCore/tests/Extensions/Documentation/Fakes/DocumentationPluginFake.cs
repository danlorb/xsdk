using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Documentation.Fakes
{
    internal class DocumentationPluginFake : PluginBase, IDocumentationPluginConfig
    {
        public void ConfigureApiDescriptions(OpenApiInfo info)
        {
            info.Title = "Sample API";
        }

        public void ConfigureSwagger(SwaggerGenOptions options) { }

        public void ConfigureSwaggerUi(SwaggerUIOptions options) { }
    }
}
