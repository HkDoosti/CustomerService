namespace CustomerService.Domain.DomainErrors;

public static class PhoneNumberError
{
    public static readonly Func<string,Error> InvalidPhoneNumber = (phoneNumber) => new Error(
        "InvalidPhoneNumber",
        $"This phone number: {phoneNumber} is invalid.");

    public static readonly Error CannotBeNullOrEmpty =  new Error(
        "CannotBeNullOrEmpty",
        $"The phone number can not be empty.");
}
