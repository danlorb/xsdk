using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;
using xSdk.Plugins.Documentation;

namespace xSdk.Demos.Builders
{
    public class DocumentationPluginBuilder : PluginBuilderBase, IDocumentationPluginBuilder
    {
        public void ConfigureApiDescriptions(Dictionary<string, OpenApiInfo> descriptions)
        {
            descriptions
                .Add("v1", new OpenApiInfo
                {
                    Title = "Sample API",
                    Version = "v1"
                });

            descriptions
                .Add("v2", new OpenApiInfo
                {
                    Title = "Sample API Test",
                    Version = "v2"
                });

            descriptions
                .Add("v3", new OpenApiInfo
                {
                    Title = "Sample API with HATEOAS Links",
                    Version = "v3"
                });
        }

        public void ConfigureSwagger(SwaggerGenOptions options) { }

        public void ConfigureSwaggerUi(SwaggerUIOptions options) { }
    }
}
