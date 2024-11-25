namespace CustomerService.Infrastructure.ValueConversions;

public class BankAccountNumberConversion : ValueConverter<BankAccountNumber, string>
{
    public BankAccountNumberConversion()
        : base(c => c.Value, c => BankAccountNumber.FromString(c))
    {

    }
}