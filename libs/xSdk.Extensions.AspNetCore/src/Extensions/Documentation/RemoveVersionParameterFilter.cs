using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace xSdk.Extensions.Documentation
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
