using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.BrandCommand;
using Shop.Aplication.Queries.BrandQueries;

namespace Shop.Api.Controllers;
[Route("api")]
public class BrandController:ControllerBase
{
    
    private readonly IMediator _mediator;

    public BrandController( IMediator mediator)
    {
        _mediator = mediator;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("brands/{page:int:min(1)}/{take:int:min(1)}")]
    public async Task<IActionResult> GetAllBrand(int page, int take)
    {
       var result= await _mediator.Send(new GetAllBrand(page, take));

       return result.Any() ? Ok(result) : NotFound("Not found brand");
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("brand/create")]
    public async Task<IActionResult> CreateBrand([FromBody]CreateBrandCommand command, CancellationToken cancellationToken = default)
    {
       var result= await _mediator.Send(command, cancellationToken);

       return Ok(result);
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("brand/edit")]
    public async Task<IActionResult> UpdateBrand(UpdateBrandCommand brandCommand, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(brandCommand, cancellationToken);

        return result is not null ? Ok(result) : BadRequest("Not found Brand Id= " + brandCommand.Id);
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("brand/delete")]
    public async Task<IActionResult> DeleteBrand(DeleteBrandCommand command, CancellationToken cancellationToken = default)
    {
       var result= await _mediator.Send(command, cancellationToken);

       return result is not null ? Ok(result) : BadRequest("Not found id " + command.Id);
    }
    
    
}