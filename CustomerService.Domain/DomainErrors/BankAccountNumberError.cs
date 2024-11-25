namespace CustomerService.Domain.DomainErrors;

public static class BankAccountNumberError
{
    public static readonly Func<string,Error> InvalidBankAccount = (bankAccount) => new Error(
        "InvalidBankAccount",
        $"This Bank account number: {bankAccount} is invalid.");

    public static readonly Error CanNotBeNullOrEmpty =  new Error(
        "InvalidBankAccount",
        $"The Bank account number can not be empty.");
}
