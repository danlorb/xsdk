using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using xSdk.Demos.Configs;
using xSdk.Extensions.Web;

namespace xSdk.Demos.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class SampleController(ILogger<SampleController> logger) : ControllerBase
    {
        /// <summary>
        /// Sends a Sample Model back
        /// </summary>
        [HttpGet(Name = "get-sample")]
        [MapToApiVersion(1)]
        [Authorize]
        [SwaggerOperation(
            Summary = "Sends a sample model back",
            Description = "Requires authentication",
            OperationId = nameof(GetSampleAsync),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult> GetSampleAsync(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Call Hello World");

                await Task.Yield();

                return Ok();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Hello World could not returned.");
                return this.BadRequestAsProblem(ex);
            }
        }

        [HttpGet("read")]
        [MapToApiVersion("1")]
        [Authorize(Policy = AuthenticationConfig.Policy_OnlyRead)]
        [SwaggerOperation(
            Summary = "Loads data for readonly users",
            Description = "Requires authentication",
            OperationId = nameof(GetOnlyReadAsync),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult> GetOnlyReadAsync(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Demostrate Read");

                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

                await Task.Yield();

                foreach (var claim in this.HttpContext.User.Claims)
                {
                    await Console.Out.WriteLineAsync(claim.ToString());
                }

                return Ok("Read was allowed");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while read.");
                return this.BadRequestAsProblem(ex);
            }
        }

        [HttpGet("write")]
        [MapToApiVersion("1")]
        [Authorize(Policy = AuthenticationConfig.Policy_ReadAndWrite)]
        [SwaggerOperation(
            Summary = "Writes data",
            Description = "Requires authentication",
            OperationId = nameof(GetReadAndWriteAsync),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult> GetReadAndWriteAsync(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Demostrate Read and Write");

                foreach (var claim in this.HttpContext.User.Claims)
                {
                    await Console.Out.WriteLineAsync(claim.ToString());
                }

                await Task.Yield();

                return Ok("Read and Write are allowed");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Error while read or write.");
                return this.BadRequestAsProblem(ex);
            }
        }
    }
}
