namespace CustomerService.Application.Validations;

public class CustomerValidation
{
    public static bool BeValidBankAccountNumber(BankAccountNumber bankAccountNumber)
    {
        if (bankAccountNumber is null)
            return false;
        return BankAccountNumber.IsValid(bankAccountNumber.Value);
    }
    public static bool BeValidEmail(Email mail)
    {
        if (mail is null)
            return false;
        return Email.IsValid(mail.Value);
    }
    public static bool BeValidPhoneNumber(PhoneNumber phoneNumber)
    {
        if (phoneNumber is null)
            return false;
        return PhoneNumber.IsValidMobileNumber(phoneNumber.Value);
    }
    public static bool BeAValidAge(DateTime dateOfBirth)
    {
        if (dateOfBirth == null)
            return false;
        var age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;
        return age >= CustomerConfig.MinAgeCustomer;
    }
}
