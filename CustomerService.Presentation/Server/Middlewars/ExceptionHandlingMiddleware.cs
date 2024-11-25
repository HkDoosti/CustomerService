namespace CustomerService.Presentation.Server.Middlewars;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
         
        _logger.LogError(exception, exception.Message);

        var code = HttpStatusCode.InternalServerError; // 500 

        string message = GetErrorMessage(exception?.InnerException?.Message ?? exception?.Message ?? "");

        Error error = new(code: "UnexpectedError", message: message);

        var result = JsonSerializer.Serialize(
            new Result(false,  error));
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }

     
    private string GetErrorMessage(string error)
    {
        if (error is null || error.Length == 0) return "";

        string errorLower = error.ToLower();

        switch (true)
        {//TODO repair hard codes
            case var _ when errorLower.Contains("customer") && errorLower.Contains("ix_customer_email"):
                return "Customer email can not be duplicate";
            case var _ when errorLower.Contains("customer") && errorLower.Contains("ix_customer_firstnamelastnamedateofbirth"):
                return "there is a customer with this name and last name and date of birth.";
            default:
                return error;
        }
    }
}
