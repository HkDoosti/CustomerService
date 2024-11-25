namespace CustomerService.Domain.DomainErrors;

public static class EmailError
{
    public static Func<string,Error> InvalidEmail = (mailAddress) => new Error(
        "InvalidEmail",
        $"This E-mail address: {mailAddress} is invalid.");

    public static Error CanNotBeNullOrEmpty =  new Error(
        "InvalidBankAccount",
        $"The E-mail address can not be empty.");
}
