using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.CommentCommand;
using Shop.Aplication.Queries;

namespace Shop.Api.Controllers;

public class CommentController:ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpGet("comments/product/{id:guid}")]
    public async Task<IActionResult> GetCommentByProductId(Guid id,[FromQuery]int skip,int take)
    {
        var result = await _mediator.Send(new GetCommentByProductId(id, skip, take));

        return result.Data != null && result.Data.Any()? Ok(result) : NotFound("Not found comment!");
    }


    [Authorize]
    [HttpPost("comment/create")]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
        
    }


    [Authorize]
    [HttpPut("comment/edit")]

    public async Task<IActionResult> EditComment(UpdateCommentCommand command)
    {
        var result = await _mediator.Send(command);

        return result ? Ok("success") : BadRequest();
    }



    [Authorize]
    [HttpDelete("comment/delete/{id:guid}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var result = await _mediator.Send(new DeleteCommentCommand(id));

        return result ? Ok("succses") : BadRequest("Comment not exist!");
    }
}