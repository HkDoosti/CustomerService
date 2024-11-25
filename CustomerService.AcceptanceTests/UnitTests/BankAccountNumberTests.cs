using CustomerService.Domain.ValueObjects;
using Xunit;

namespace CustomerService.AcceptanceTests.UnitTests;

public class BankAccountNumberTests
{
    [Theory]
    [InlineData("1234569871", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("hhj456", false)]
    [InlineData("pkjgtrdcvb", false)]
    [InlineData("123654", false)]
    public void IsValidBankAccountNumber_ShouldReturnExpectedReturn(string inputBankAccountNumber, bool expectedValidity)
    {
        //Act
        bool isValid = BankAccountNumber.IsValid(inputBankAccountNumber);

        Assert.Equal(expectedValidity, isValid);
    }
}
