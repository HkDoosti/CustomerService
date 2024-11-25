using FluentAssertions;
using CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;
using CustomerService.Application.Commands.CustomerCommands.DeleteCustomerCommand;
using CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;
using CustomerService.Domain.Dto.CustomerDotes;
using CustomerService.Domain.Dto.CustomerDtos;
using CustomerService.Domain.Entities;
using CustomerService.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CustomerService.AcceptanceTests.IntegrationTests;

public class CustomerControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<ISender> _mockSender;
    public CustomerControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        
        _client = factory.CreateClient();
        _mockSender = new Mock<ISender>();
    }

  

    [Fact]
    public async Task Customer_CRUD_Test()
    {
        // Step 1: Add a new customer  
        DateTime bDate = DateTime.UtcNow
           .AddYears(-1 * (CustomerConfig.MinAgeCustomer + 5));

        var cancellationToken = new CancellationToken();

        var addCustomerRequest = new AddCustomerCommandRequest
            (Customer: new CreateCustomerDto
            (FirstName: "CrudTest",
            LastName: "Doosti",
            DateOfBirth: bDate,
            PhoneNumber: new CustomerService.Domain.ValueObjects.PhoneNumber("+989137210743"),
            Email: new CustomerService.Domain.ValueObjects.Email("CrudTest@gmail.com"),
            BankAccountNumber: new CustomerService.Domain.ValueObjects.BankAccountNumber("1472583695")));

        CustomerDto customerDto = new CustomerDto(
           Id: Guid.NewGuid(),
           FirstName: "CrudTest",
          LastName: "Doosti",
          DateOfBirth: bDate,
          PhoneNumber: new CustomerService.Domain.ValueObjects.PhoneNumber("+989137210743"),
          Email: new CustomerService.Domain.ValueObjects.Email("CrudTest@gmail.com"),
          BankAccountNumber: new CustomerService.Domain.ValueObjects.BankAccountNumber("1472583695"));

        var expectedResult = Result.Success(customerDto); // Assuming Result has a Success method  
        _mockSender.Setup(sender => sender.Send(addCustomerRequest, cancellationToken))
            .ReturnsAsync(expectedResult);
         
        var addResponse = await  _client.PostAsJsonAsync("/api/Customer/Add", addCustomerRequest);
        addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdCustomer = await addResponse.Content.ReadFromJsonAsync<Result<CustomerDto>>();
        createdCustomer.Should().NotBeNull();
         createdCustomer.Value.FirstName.Should().Be("CrudTest"); 

        // Step 2: Get the customer by ID  
        var getResponse = await _client.GetAsync($"/api/Customer/GetById?Id={createdCustomer.Value.Id}");
        

        var fetchedCustomer = await getResponse.Content.ReadFromJsonAsync<Result<CustomerDto>>();
        fetchedCustomer.Should().NotBeNull();
        fetchedCustomer.Value.FirstName.Should().Be("CrudTest");

        //// Step 3: Edit the customer  
         
        var editCustomerRequest = new EditCustomerCommandRequest
            (Customer: fetchedCustomer.Value);


        var editResponse = await _client.PostAsJsonAsync("/api/Customer/Edit", editCustomerRequest);
        editResponse.StatusCode.Should().Be(HttpStatusCode.Created);



        // Step 4: Delete the customer  
        DeleteCustomerCommandRequest deleteCustomerCommandRequest = new DeleteCustomerCommandRequest(Id: createdCustomer.Value.Id);
        var deleteResponse = await _client.PostAsJsonAsync("/api/Customer/Delete", deleteCustomerCommandRequest);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //// Verify deletion  
        var verifyDeleteResponse = await _client.GetAsync($"/api/Customer/GetById?Id={createdCustomer.Value.Id}");
        

        var deletedResult = await verifyDeleteResponse.Content.ReadFromJsonAsync<Result<CustomerDto>>();

        deletedResult.Error.Message.Should().Be("Customer.NotFound");


    }



}
