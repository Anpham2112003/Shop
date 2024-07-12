using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.ProductCommand;
using Shop.Aplication.Queries;


namespace Shop.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductController:ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

   

    [HttpGet("product/{id:guid}")]
    public async Task<IActionResult> GetProductDetail(Guid id)
    {
        var result = await _mediator.Send(new GetProductDetail() { Id = id });
        
        return result is null ? BadRequest("Product not found ") : Ok(result);
    }
    
    [HttpGet("product/previews")]
    public async Task<IActionResult> GetProductPreview([FromQuery]int page, int take)
    {
        var result = await _mediator.Send(new GetProductsPreview(page,take));

        return result is null ? NotFound("Not found product!") : Ok(result);
    }

    [HttpGet("product/search")]
    public async Task<IActionResult> SearchProduct([FromQuery]string name,int skip,int take)
    {
        var result = await _mediator.Send(new SearchProduct(name,skip,take));

        return result.Data!.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("product/create")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand productCommand, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(productCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPut("product/edit")]
    public async Task<IActionResult> UpdateProductCommand([FromForm]UpdateProductCommand command)
    {
        
        var result = await _mediator.Send(command);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpDelete("product/delete/{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));

        return result ? Ok("success") : BadRequest("Product not exist!");
    }
   
    
}