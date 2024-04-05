using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.OrderCommand;
using Shop.Aplication.Queries.OrderQueries;

namespace Shop.Api.Controllers;

[Route("api")]
public class OrderController:ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize]    
    
    [HttpGet("order/{id:guid}")]
    public async Task<IActionResult> GetOrderDetail(Guid id)
    {
        var result = await _mediator.Send(new GetOrderDetail(id));
        
        return result is null ? NotFound() : Ok(result);
    }
    
    
    [HttpGet("orders/{id:guid}/{page:int:min(1)}/{take:int:range(1,20)}")]
    public async Task<IActionResult> GetOrderSuccess(Guid id,int page , int take)
    {
        var result = await _mediator.Send(new GetOrdersByUserId(id, page, take));

        return result is null ? NotFound() : Ok(result);
    }
    [Authorize]    
    
    [HttpPost("order/add")]
    public async Task<IActionResult> AddOrder(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
    }



    [Authorize]    
    [HttpDelete("order/remove/{id:guid}")]
    public async Task<IActionResult> RemoveOrder(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));

        return result ? Ok("success") : BadRequest("");
    }
    
    
    
}