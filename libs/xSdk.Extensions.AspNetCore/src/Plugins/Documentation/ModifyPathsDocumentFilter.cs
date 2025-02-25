using System;
using System.IO;
using System.Reflection;
using Asp.Versioning;
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
            this.showVariableDocu = SlimHost.Instance.VariableSystem.GetSetup<DocumentationSetup>().ShowVariableDocumentation;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                if (IsValid(path.Key) && IsVersionValid(path.Key, swaggerDoc, context))
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

        private bool IsVersionValid(string path, OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            var attributes = context
                .ApiDescriptions.Where(x => x.RelativePath == path)
                .Select(x => x.ActionDescriptor.EndpointMetadata.FirstOrDefault(y => y.GetType() == typeof(MapToApiVersionAttribute)))
                .Cast<MapToApiVersionAttribute>();

            if (attributes != null && attributes.Any())
            {
                return attributes.Any(x => x.Versions.Any(y => string.Format("v{0}", y.MajorVersion) == swaggerDoc.Info.Version));
            }

            return false;
        }
    }
}
