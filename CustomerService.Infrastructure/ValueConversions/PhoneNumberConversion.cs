namespace CustomerService.Infrastructure.ValueConversions;

public class PhoneNumberConversion: ValueConverter<PhoneNumber, string>
{
    public PhoneNumberConversion() 
        : base(c => c.Value, c => PhoneNumber.FromString(c))
    {
        
    }
}
