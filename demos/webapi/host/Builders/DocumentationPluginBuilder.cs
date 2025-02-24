using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Documentation;

namespace xSdk.Demos.Builders
{
    public class DocumentationPluginBuilder : PluginBuilderBase, IDocumentationPluginBuilder
    {
        public void ConfigureApiDescriptions(OpenApiInfo info)
        {
            info.Title = "Sample API";
            info.Version = "v1";
        }

        public void ConfigureSwagger(SwaggerGenOptions options)
        {
            
        }

        public void ConfigureSwaggerUi(SwaggerUIOptions options)
        {
            
        }
    }
}
