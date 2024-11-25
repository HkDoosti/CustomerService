namespace CustomerService.Presentation.Server.Controllers;


public class BaseController(ISender sender) : ControllerBase
{

    public readonly ISender _sender = sender;
    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => BadRequest(
                CreateProblemDetail(
                "Validation Error",
                StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors
                )),
            _ => BadRequest(
                CreateProblemDetail(
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error
                ))
        };

    private static ProblemDetails CreateProblemDetail(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }

        };

}
