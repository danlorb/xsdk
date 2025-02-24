using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using xSdk.Hosting;

namespace xSdk.Plugins.Documentation
{
    public class ModifyPathsDocumentFilter : IDocumentFilter
    {
        private readonly bool showVariableDocu;

        public ModifyPathsDocumentFilter()
        {
            this.showVariableDocu = SlimHost
                .Instance.VariableSystem.GetSetup<DocumentationSetup>()
                .ShowVariableDocumentation;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                if (IsValid(path.Key))
                {
                    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
                }
            }
            swaggerDoc.Paths = paths;
        }

        private bool IsValid(string value)
        {
            if (showVariableDocu)
            {
                return true;
            }

            // Remove Variable Documentation
            return value.IndexOf("/variable/") == -1 && value.Length > 1;
        }
    }
}
