using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.AddressCommand;
using Shop.Aplication.Queries;

namespace Shop.Api.Controllers;
[ApiController]
[Route("api")]
public class AddressController:ControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("address")]
    public async Task<IActionResult> GetAddressByUserId()
    {
        var result = await _mediator.Send(new GetMyAddress());

        return !result.Any() ? NotFound("Not found address!") : Ok(result);
    }

    
    [HttpPost("address/add")]
    public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok(result) : BadRequest();
    }
    
    [HttpPut("address/edit")]
    public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
    }
    
    [HttpDelete("address/remove/{id:guid}")]
    public async Task<IActionResult> RemoveAddress(Guid id)
    {
        var result = await _mediator.Send(new DeleteAddressCommand(id));

        return result ? Ok("success") : BadRequest();
    }
     
}