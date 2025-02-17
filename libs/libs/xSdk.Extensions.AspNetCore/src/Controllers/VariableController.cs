using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using xSdk.Extensions.Variable;
using xSdk.Extensions.Web;

namespace xSdk.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VariableController(IVariableService variableSvc, ILogger<HealthController> logger)
        : ControllerBase
    {
        /// <summary>
        /// Gets all configured variables
        /// </summary>
        [HttpGet("definition")]
        [MapToApiVersion(1)]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IDictionary<string, object>>> GetVariables(
            CancellationToken token = default
        )
        {
            try
            {
                logger.LogDebug("Get all variable definitions");

                await Task.Yield();

                var result = new List<object>();
                foreach (var variable in variableSvc.Variables)
                {
                    result.Add(
                        new
                        {
                            variable.Name,
                            variable.HelpText,
                            variable.Prefix,
                            variable.IsHidden,
                            variable.IsProtected,
                            variable.NoPrefix,
                        }
                    );
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Variables could not loaded");
                return this.BadRequestAsProblem(ex);
            }
        }

        /// <summary>
        /// Gets resources namens for open telemetry
        /// </summary>
        [HttpGet("resource")]
        [MapToApiVersion(1)]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDictionary<string, object>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IDictionary<string, object>>> GetResourceNames(
            CancellationToken token = default
        )
        {
            try
            {
                logger.LogDebug("Get all variable resource names");

                await Task.Yield();

                var resourceNames = variableSvc.BuildResources();

                return Ok(resourceNames);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Variable resource names not loaded");
                return this.BadRequestAsProblem(ex);
            }
        }

        /// <summary>
        /// Gets current environment Values
        /// </summary>
        [HttpGet("value")]
        [MapToApiVersion(1)]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDictionary<string, object>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IDictionary<string, object>>> GetValues(
            CancellationToken token = default
        )
        {
            try
            {
                logger.LogDebug("Get all variable with values");

                await Task.Yield();

                var variables = variableSvc.ToDictionary();

                return Ok(variables);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Variables with values could not loaded");
                return this.BadRequestAsProblem(ex);
            }
        }
    }
}
