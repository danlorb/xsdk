using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using xSdk.Demos.Builders;
using xSdk.Demos.Data;
using xSdk.Extensions.Links;
using xSdk.Extensions.Web;

namespace xSdk.Demos.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiVersion(3)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class SampleController(ILinksService linksService, ILogger<SampleController> logger) : ControllerBase
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
            OperationId = nameof(GetSampleAsyncv1),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult> GetSampleAsyncv1(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Call Hello World from v1");

                await Task.Yield();

                return Ok("Hello World from v1");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Hello World could not returned.");
                return this.BadRequestAsProblem(ex);
            }
        }

        // <summary>
        /// Sends a Sample Model back
        /// </summary>
        [HttpGet(Name = "get-sample")]
        [MapToApiVersion(2)]
        [Authorize]
        [SwaggerOperation(
            Summary = "Sends a sample model back",
            Description = "Requires authentication",
            OperationId = nameof(GetSampleAsyncv2),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult> GetSampleAsyncv2(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Call Hello World from v2");

                await Task.Yield();

                return Ok("Hello World from v2");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Hello World could not returned.");
                return this.BadRequestAsProblem(ex);
            }
        }

        // <summary>
        /// Sends a Sample Model back
        /// </summary>
        [HttpGet(Name = "get-sample")]
        [MapToApiVersion(3)]
        //[Authorize]
        [SwaggerOperation(
            Summary = "Sends a sample model back",
            Description = "Requires authentication",
            OperationId = nameof(GetSampleAsyncv2),
            Tags = new[] { "Sample" }
        )]
        public async Task<ActionResult<SampleModel>> GetSampleAsyncv3(CancellationToken token = default)
        {
            try
            {
                logger.LogDebug("Call get Sample with Hateoas Links");

                var model = new SampleModelLinks
                {
                    Name = "Hello World from v3",
                };

                await linksService.AddLinksAsync(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "API: Hello World could not returned.");
                return this.BadRequestAsProblem(ex);
            }
        }

        [HttpGet("read")]
        [MapToApiVersion("1")]
        [Authorize(Policy = AuthenticationPluginBuilder.Policy_OnlyRead)]
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
        [Authorize(Policy = AuthenticationPluginBuilder.Policy_ReadAndWrite)]
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

