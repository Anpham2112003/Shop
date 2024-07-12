using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.OrderCommand;
using Shop.Aplication.Queries;

namespace Shop.Api.Controllers;
[ApiController]
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


    [Authorize]
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrderSuccess(int page , int take)
    {
        var result = await _mediator.Send(new GetOrders( page, take));

        return result is null ? NotFound() : Ok(result);
    }


    [Authorize]    
    
    [HttpPost("order/create")]
    public async Task<IActionResult> AddOrder(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return result is null ? BadRequest() : Ok(result);
    }

    [Authorize]
    [HttpDelete("order/delete")]
    public async Task<IActionResult> RemoveOrder(RemoveOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return result == null ? BadRequest() : Ok(result);
    }



    
    
    
    
}