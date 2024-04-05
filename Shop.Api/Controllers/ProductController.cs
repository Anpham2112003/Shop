using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.ProductCommand;
using Shop.Aplication.Queries.ProductQueries;



namespace Shop.Api.Controllers;
[Route("api")]
public class ProductController:ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("product/create")]
    public async Task<IActionResult> CreateProduct( CreateProductCommand productCommand,CancellationToken cancellationToken=default)
    {
        var result=await _mediator.Send(productCommand,cancellationToken );
        
        return Ok(result);
    }

    [HttpGet("product/{id:guid}")]
    public async Task<IActionResult> GetProductDetail(Guid id)
    {
        var result = await _mediator.Send(new GetProductById() { Id = id });
        
        return result is null ? BadRequest("Product not found ") : Ok(result);
    }
    
    [HttpGet("products/preview/{page:int:min(1)}/{take:int:min(1)}")]
    public async Task<IActionResult> GetProductPreview(int page, int take)
    {
        var result = await _mediator.Send(new GetAllProductPreview(page,take));

        return result is null ? NotFound("Not found product!") : Ok(result);
    }
    
    [HttpGet("products/brand/{id:guid}/{page:int:min(1)}/{take:int:min(1)}")]
    public async Task<IActionResult> GetProductByBrandId(Guid id, int page, int take)
    {
        var result = await _mediator.Send(new GetProductsByBrandId(id, page, take));

        return result is null ? NotFound("Not found product! ") : Ok(result);
    }
    
    [HttpPut("product/edit")]
    public async Task<IActionResult> UpdateProductCommand(Guid id,UpdateProductCommand command)
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
    [HttpGet("product/category/{id:guid}/{page:int:min(1)}/{take:int:range(1,20)}")]
    public async Task<IActionResult> GetProductByCategoryId(Guid id,int page,int take)
    {
        var result = await _mediator.Send(new GetProductsByCategoryId(id,page , take));
        return result is not null? Ok(result) : NotFound();
    }
    
}