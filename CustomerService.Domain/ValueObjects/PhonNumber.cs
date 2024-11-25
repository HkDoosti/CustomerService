using CustomerService.Domain.DomainErrors;

namespace CustomerService.Domain.ValueObjects;

public class PhoneNumber: BaseValueObject<PhoneNumber>
{
    public static int MaxLength = 20;
    private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

    public string Value { get; private set; }
    public static PhoneNumber FromString(string value) => new (value);
     
    public PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string number)=>
        IsValidMobileNumber(number)? new PhoneNumber(number):
        throw new CustomException(PhoneNumberError.InvalidPhoneNumber(number??"").Message);
    
    public static bool IsValidMobileNumber(string number)
    {
        try
        {
            var phoneNumber = PhoneUtil.Parse(number, null);

            var regionCode = PhoneUtil.GetRegionCodeForNumber(phoneNumber);

            return PhoneUtil.IsValidNumber(phoneNumber) &
                 (PhoneUtil.GetNumberType(phoneNumber) == PhoneNumberType.FIXED_LINE_OR_MOBILE ||
                 PhoneUtil.GetNumberType(phoneNumber) == PhoneNumberType.MOBILE);
             
             
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    public override string ToString() => Value;
    
}
