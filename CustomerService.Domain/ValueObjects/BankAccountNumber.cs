using CustomerService.Domain.DomainErrors;

namespace CustomerService.Domain.ValueObjects;

public class BankAccountNumber: BaseValueObject<BankAccountNumber>
{
    public static  int MaxLength = 10;
    public string Value { get; private set; }
    public static BankAccountNumber FromString(string value) => new(value);

    public BankAccountNumber(string value)=>Value = value;
    
    public static BankAccountNumber Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new CustomException(BankAccountNumberError.CanNotBeNullOrEmpty.Message);
        }

        if (!IsValid(value))
        {
            throw new CustomException(BankAccountNumberError.InvalidBankAccount(value).Message);
        }

        else return new(value);
    }

    public static bool IsValid(string value)
    {
        if (value is null )
        {
            return false;
        }
        // validation logic     
        return   value.Length == 10 && value.All(char.IsDigit); 
    }

     public override bool Equals(object obj)
    {
        if (obj is BankAccountNumber other)
        {
            return Value == other.Value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
