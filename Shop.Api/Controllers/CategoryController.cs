using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.CategoryCommand;
using Shop.Aplication.Queries.CategoryQueries;

namespace Shop.Api.Controllers;

[Route("api")]
public class CategoryController:ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
      
    
    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategory()
    {
        var result = await _mediator.Send(new GetAllCategory());
        
        return result != null && result.Any() ? Ok(result) : NotFound();
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("category/create")]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("category/edit")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        return result != null ? Ok(result) : BadRequest("Category not exist!");
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("category/delete/{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));

        return result ? Ok("success") : BadRequest("Category not exist!");
    }
}