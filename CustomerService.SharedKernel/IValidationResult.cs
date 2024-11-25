﻿
namespace CustomerService.SharedKernel;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError", 
        "A validation error occurred"
        );
    Error[] Errors { get; }
}
