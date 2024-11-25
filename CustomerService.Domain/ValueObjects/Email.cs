
using CustomerService.Domain.DomainErrors;
using System.Text.RegularExpressions;

namespace CustomerService.Domain.ValueObjects;

public class Email: BaseValueObject<Email>
{
    public static int MaxLength = 100;
    public string Value { get; private set; }
    public static Email FromString(string value) => new(value);
    public Email(string value) => Value = value;

    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static Email Create(string mailAddress) =>
        IsValid(mailAddress) ? new Email(mailAddress) :
           throw new CustomException(EmailError.InvalidEmail(mailAddress).Message);

    public static bool IsValid(string mailAddress)
    {
        try
        {
            if (string.IsNullOrEmpty(mailAddress)) 
                return false;

             return EmailRegex.IsMatch(mailAddress);    
        }
        catch  (Exception ex)
        {
            return false;
        }
    }
}
