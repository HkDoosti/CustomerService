using CustomerService.Domain.ValueObjects;
using Xunit;


namespace CustomerService.AcceptanceTests.UnitTests;

public class PhoneNumberTests
{
    
    [Theory]
    [InlineData("+1234567890", false)]   // Example of a valid mobile number   
    [InlineData("+982188776655", false)]   // Example of a valid mobile number   
    [InlineData("+989121234567", true)]   // Example of a valid mobile number   
    [InlineData("+16156381234", true)]   // Example of a valid mobile number   
    [InlineData("+31612683550", true)] // Valid German mobile number  
    [InlineData("+989137210743", true)] // Valid Iran mobile number  
    [InlineData("12345", false)]         // Invalid number (too short)  
    [InlineData("+0000000000", false)]   // Invalid number (not a valid prefix)  
    [InlineData("abcd1234", false)]      // Invalid number (non-numeric)  
    [InlineData("", false)]               // Invalid number (empty string)  
    [InlineData(null, false)]             // Invalid input (null)  
    public void IsValidMobileNumber_ShouldReturnExpectedResult(string inputNumber, bool expectedValidity)
    {
        // Act  
        bool isValid = PhoneNumber.IsValidMobileNumber(inputNumber);

        // Assert  
        Assert.Equal(expectedValidity, isValid);
    }

}
