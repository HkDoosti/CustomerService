namespace CustomerService.Infrastructure.ValueConversions;

public class EmailConversion : ValueConverter<Email, string>
{
    public EmailConversion()
        : base(c => c.Value, c => Email.FromString(c))
    {

    }
}
