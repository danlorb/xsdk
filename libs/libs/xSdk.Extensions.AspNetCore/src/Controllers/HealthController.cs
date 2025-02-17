using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using xSdk.Extensions.Web;

namespace xSdk.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HealthController(ILogger<HealthController> logger) : ControllerBase
    {
        /// <summary>
        /// Gets current status of API. Could be used for Health Checks
        /// </summary>
        [HttpGet()]
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetStatus(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Get status for API");

                await Task.Yield();

                return Ok("ok");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Status could not loaded");
                return this.BadRequestAsProblem(ex);
            }
        }

        /// <summary>
        /// Ping the API and await a pong
        /// </summary>
        [HttpGet("ping")]
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetPong(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Pong requested");

                await Task.Yield();

                return Ok("pong");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Pong could not sent");
                return this.BadRequestAsProblem(ex);
            }
        }
    }
}
