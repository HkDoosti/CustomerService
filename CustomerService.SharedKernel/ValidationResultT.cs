﻿namespace CustomerService.SharedKernel;

public class ValidationResult<TValue> :
    Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;
    
    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Error[] errors) =>
      new ValidationResult<TValue>(errors);

    public Result<TValue> GetResult() 
    {
      return this;
    }
   

}
