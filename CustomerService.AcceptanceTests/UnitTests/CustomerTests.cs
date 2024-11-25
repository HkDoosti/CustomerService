using FluentAssertions;
using CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;
using CustomerService.Application.Commands.CustomerCommands.DeleteCustomerCommand;
using CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;
using CustomerService.Domain.Dto.CustomerDotes;
using CustomerService.Domain.Dto.CustomerDtos;
using CustomerService.Domain.Entities;
using CustomerService.Presentation.Server.Controllers;
using CustomerService.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

public class CustomerControllerTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mockSender = new Mock<ISender>();
        _controller = new CustomerController(_mockSender.Object);
    }


    [Fact]
    public async Task AddCustomer_ReturnsCreatedResponse()
    {
        // Arrange
        DateTime bDate = DateTime.UtcNow
          .AddYears(-1 * (CustomerConfig.MinAgeCustomer + 5));
        var cancellationToken = new CancellationToken();
        var newCustomer = new AddCustomerCommandRequest
            (Customer: new CreateCustomerDto
            (FirstName: "Hakime",
            LastName: "Doosti",
            DateOfBirth: bDate,
            PhoneNumber: new CustomerService.Domain.ValueObjects.PhoneNumber("+989137210743"),
            Email: new CustomerService.Domain.ValueObjects.Email("Hakimedusti@gmail.com"),
            BankAccountNumber: new CustomerService.Domain.ValueObjects.BankAccountNumber("1472583695")));

        // Create a mock result to return from the handler
        CustomerDto customerDto = new CustomerDto(
            Id: Guid.NewGuid(),
            FirstName: "Hakime",
           LastName: "Doosti",
           DateOfBirth: bDate,
           PhoneNumber: new CustomerService.Domain.ValueObjects.PhoneNumber("+989137210743"),
           Email: new CustomerService.Domain.ValueObjects.Email("Hakimedusti@gmail.com"),
           BankAccountNumber: new CustomerService.Domain.ValueObjects.BankAccountNumber("1472583695"));

        var expectedResult = Result.Success(customerDto); // Assuming Result has a Success method  
        _mockSender.Setup(sender => sender.Send(newCustomer, cancellationToken))
            .ReturnsAsync(expectedResult);

        // Act  
        var response = await _controller.Add(newCustomer, cancellationToken);

        // Assert  
        response.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = response as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);  // Check for HTTP 201 Created  
        createdResult?.Value.Should().Be(expectedResult);
    }


    [Fact]
    public async Task EditCustomer_ReturnsCreatedResponse()
    {
        // Arrange
        DateTime bDate = DateTime.UtcNow
          .AddYears(-1 * (CustomerConfig.MinAgeCustomer + 5));
        var cancellationToken = new CancellationToken();
        var customer = new EditCustomerCommandRequest
            (Customer: new CustomerDto
            (Id: Guid.Parse("1f5ef4d1-7b84-4e9c-9d16-c1e6c6c5d1a3"),
            FirstName: "Hakime",
            LastName: "Doosti",
            DateOfBirth: bDate,
            PhoneNumber: new CustomerService.Domain.ValueObjects.PhoneNumber("+989137211743"),
            Email: new CustomerService.Domain.ValueObjects.Email("Hakimedusti@yahoo.com"),
            BankAccountNumber: new CustomerService.Domain.ValueObjects.BankAccountNumber("1472583695")));

        // Create a mock result to return from the handler  
        var expectedResult = Result.Success(); // Assuming Result has a Success method  
        _mockSender.Setup(sender => sender.Send(customer, cancellationToken))
            .ReturnsAsync(expectedResult);

        // Act  
        var response = await _controller.Edit(customer, cancellationToken);

        // Assert  
        response.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = response as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);  // Check for HTTP 201 Created  
        createdResult?.Value.Should().Be(expectedResult);
    }


    [Fact]
    public async Task DeleteCustomer_ReturnsCreatedResponse()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var customer = new DeleteCustomerCommandRequest
              (Id: Guid.Parse("1f5ef4d1-7b84-4e9c-9d16-c1e6c6c5d1a3"));
        // Create a mock result to return from the handler  
        var expectedResult = Result.Success(); // Assuming Result has a Success method  
        _mockSender.Setup(sender => sender.Send(customer, cancellationToken))
            .ReturnsAsync(expectedResult);

        // Act  
        var response = await _controller.Delete(customer, cancellationToken);

        // Assert  
        response.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = response as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);  // Check for HTTP 201 Created  
        createdResult?.Value.Should().Be(expectedResult);
    }
}
