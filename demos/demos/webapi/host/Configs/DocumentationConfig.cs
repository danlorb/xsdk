using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Documentation;
using xSdk.Extensions.Plugin;

namespace xSdk.Demos.Configs
{
    public class DocumentationConfig : PluginBase, IDocumentationPluginConfig
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
