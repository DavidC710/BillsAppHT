
namespace BillsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected ActionResult FromResult<T>(Result<T> result) => result.ResultType switch
        {
            ResultType.Ok => this.Ok(result.Data),
            ResultType.NotFound => this.NotFound(result.Errors),
            ResultType.Invalid => this.BadRequest(result.Errors),
            ResultType.Unexpected => this.BadRequest(result.Errors),
            ResultType.Unauthorized => this.Unauthorized(result.Errors),
            ResultType.PermissionDenied => this.StatusCode(403, result.Errors), // Replaces Forbid(). This gives us option to send result.Errors in payload.
            ResultType.PartialOk => this.Ok(result.Data),
            _ => throw new Exception("Unhandled result."),
        };
    }
}
