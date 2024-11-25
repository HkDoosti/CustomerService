using System.Text.Json;

namespace CustomerService.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {

            return (CreateValidationResult<TResponse>(errors)) ; 
        }
        return await next();
    }

    private TResponse GetError(Error[] errors)
    {
        throw new NotImplementedException();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
      where TResult :  Result
    {

        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }
        
        var validationResultType = typeof(ValidationResult<TResult>);
        var method = validationResultType.GetMethod(nameof(ValidationResult<TResult>.WithErrors));

        if (method == null)
        {
            throw new InvalidOperationException($"Method '{nameof(ValidationResult<TResult>.WithErrors)}' not found on type '{validationResultType.FullName}'.");
        }

        var validationResult = method.Invoke(null, new object?[] { errors });
        if (validationResult is TResult result)
        {
            return result;
        }
        if (validationResult is ValidationResult<TResult> validationResultOfTResult)
        {
            var result2 = validationResultOfTResult.GetResult();

           
            // Explicitly checking if the result type matches   
            if (result2 is TResult validResult)
            {
                
                return result2 as TResult;//(ValidationResult<TResult>.WithErrors(errors) as Result<TResult> as TResult)!;
            }
             if(typeof(TResult) == typeof(Result<CustomerDto>))
                return (ValidationResult<CustomerDto>.WithErrors(errors) as TResult)!;

        }
        throw new InvalidCastException($"Unable to cast '{validationResult?.GetType().FullName}' to '{typeof(TResult).FullName}'.");

    }
}
