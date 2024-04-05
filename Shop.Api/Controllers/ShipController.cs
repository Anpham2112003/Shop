using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.ShipCommand;
using Shop.Aplication.Commands.ShipCommnd;
using Shop.Aplication.Queries;

namespace Shop.Api.Controllers;
[Route("api")]
public class ShipController:ControllerBase
{
    private readonly IMediator _mediator;

    public ShipController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Authorize]
    [HttpGet("ship/{id:guid}")]
    public async Task<IActionResult> GetShip(Guid id)
    {
        var result = await _mediator.Send(new GetInfoShip(id));

        return result is null?NotFound():Ok(result);
    }


    [Authorize]
    [HttpPost("ship/add")]
    public async Task<IActionResult> AddShip(CreateShipCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
    }


    [Authorize(Roles ="Admin")]
    [HttpPatch("ship/edit")]
    public async Task<IActionResult> EditStateShip(UpdateShipCommand command)
    {
        
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
    }

    [Authorize]
    [HttpPut("ship/address/edit")]
    public async Task<IActionResult> EditAddress(UpdateAddressShipCommand command)
    {
        var result = await _mediator.Send(command);

        return result?Ok("Success"):BadRequest();
    }


    [Authorize]
    [HttpDelete("ship/remove/{id:guid}")]
    public async Task<IActionResult> RemoveShip(Guid id)
    {
        var result =await _mediator.Send(new DeleteShipCommand(){Id = id});

        return result ? Ok("success") : BadRequest();
    }
}