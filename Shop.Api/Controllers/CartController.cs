using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.CartCommand;
using Shop.Aplication.Queries.CartQueries;

namespace Shop.Api.Controllers;

[Route("api")]
public class CartController:ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [Authorize]    
    [HttpGet("cart/user/{id:guid}/{page:int}/{take:int}")]
    public async Task<IActionResult> GetCartByUserId(Guid id,int page,int take)
    {
        var result = await _mediator.Send(new GetCartByUserId(id, page, take));

        return result.Data != null && result.Data.Any() ? Ok(result) : NotFound();
    }



    [Authorize]
    [HttpPost("cart/add")]
    public async Task<IActionResult> AddToCart([FromBody]CreateCartCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest(" UserId or ProductId is not exist !");
    }


    [Authorize]
    [HttpPut("cart/edit")]
    public async Task<IActionResult> EditCart(UpdateCartCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest("Cart not exist!");
        
    }


    [Authorize]
    [HttpDelete("cart/delete/{id:guid}")]
    public async Task<IActionResult> RemoveCart(Guid id)
    {
        var result = await _mediator.Send(new DeleteCartCommand(id));
        
        return result ? Ok("success") : BadRequest("Cart not exist!");
    }
}