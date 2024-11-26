using CustomerService.Application.Queries.CustomerQueries.GetCustomerByIdQuery;
using CustomerService.Domain.Dto.CustomerDotes;

namespace CustomerService.Presentation.Server.Controllers;


[Route("api/[controller]/[Action]")]
[ApiController]
public class CustomerController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Add(AddCustomerCommandRequest addCustomer, CancellationToken cancellationToken)
    {

        Result<CustomerDto> res = await _sender.Send(addCustomer, cancellationToken);
        if (res is null)
        {
            return CreatedAtAction(nameof(Add), new { IsSuccess = false }, res);
        }
        if (res.IsFailure)
        {
            return HandleFailure(res);
        }
        return CreatedAtAction(nameof(Add), new { IsSuccess = res.IsSuccess }, res);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteCustomerCommandRequest deleteCustomer, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(deleteCustomer, cancellationToken);
        if (res.IsFailure)
        {
            return HandleFailure(res);
        }
        return CreatedAtAction(nameof(Delete), new { IsSuccess = res.IsSuccess }, res);
    }
    [HttpPut]
    public async Task<IActionResult> Edit(EditCustomerCommandRequest editCustomer, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(editCustomer, cancellationToken);
        if (res.IsFailure)
        {
            return HandleFailure(res);
        }
        return CreatedAtAction(nameof(Edit), new { IsSuccess = res.IsSuccess }, res);
    }
    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var getById = new GetCustomerByIdQueryRequest(Id: Id);
        Result<CustomerDto> res = await _sender.Send(getById, cancellationToken);
        if (res is null)
        {
            return CreatedAtAction(nameof(GetById), new { IsSuccess = true }, res);
        }
        if (res.IsFailure)
        {
            return HandleFailure(res);
        }
        return CreatedAtAction(nameof(GetById), new { IsSuccess = res.IsSuccess }, res);
    }

}
