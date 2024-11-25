using CustomerService.Domain.ValueObjects;
using Xunit;

namespace CustomerService.AcceptanceTests.UnitTests;

public class EmailTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("s@yahoo.com", true)]
    [InlineData("dd@gmail.com", true)]
    [InlineData("ddgmail.com", false)]
    [InlineData("dd@gmailcom", false)]
    [InlineData("ddgmailcom", false)]
    [InlineData("1111111", false)]
    public void IsValidEmail_ShouldReturnExpectedReturn(string inputValue, bool expected)
    {
        //Act
        bool isValid = Email.IsValid(inputValue);

        Assert.Equal(expected, isValid);
    }
}
