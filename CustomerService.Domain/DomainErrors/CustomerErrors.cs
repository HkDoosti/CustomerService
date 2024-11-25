using CustomerService.Domain.Entities;

namespace CustomerService.Domain.DomainErrors;

public static class CustomerErrors
{
    public static readonly Func<string, Error> NotBeNull = (propertyName) => new Error(
            "Customer.NotBeNull",
            $"The {propertyName} can not be null.");

    public static readonly Func<string, Error> NotBeEmpty = (propertyName) => new Error(
            "Customer.NotBeEmpty",
            $"The {propertyName} can not be Empty.");

    public static readonly Func<string,int, Error> InvalidLength = (propertyName,maxLength) => new Error(
           "Customer.InvalidLength",
           $"Length {propertyName} should not be bigger than {maxLength}.");

    public static readonly Func<Guid, Error> NotFound = (id) => new Error(
            "Customer.NotFound",
            $"The Customer with the identifier {id} was not found.");

    public static Func<string, Error> AllocatedEmail = (mailAddress) => new Error(
       "Customer.AllocatedEmail",
            $"There is a customer with this Email {mailAddress}.");

    public static Func<string,string,string, Error> ExistsCustomer = (firstName, lastName, dateOfBirth) => new Error(
        "Customer.ExistsCustomer",
            $"There is a customer with this name: {firstName} and last name: {lastName} and date of birth: {dateOfBirth}.");

    public static Error NotValidAge =   new Error(
        "Customer.ExistsCustomer",
            $"Customer must be at least {CustomerConfig.MinAgeCustomer} years old.");


}
