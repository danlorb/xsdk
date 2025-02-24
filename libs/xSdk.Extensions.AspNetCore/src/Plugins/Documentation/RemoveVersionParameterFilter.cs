using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace xSdk.Plugins.Documentation
{
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p =>
                string.Compare(p.Name, "version", true) == 0
            );
            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }
}
