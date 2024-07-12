using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands.TagCommand;
using Shop.Aplication.Queries;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController:ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("tags")]
        [Authorize()]
        public async Task<IActionResult> GetAllTag()
        {
            var result = await _mediator.Send(new GetListTag());

            return result.Any() ? Ok(result) : BadRequest();
        }


        [HttpGet("{id:guid}/products")]
        public async Task<IActionResult> GetProductByTagId(Guid id,[FromQuery] int page,int take)
        {
            var result = await _mediator.Send(new GetProductsByTagId(id, page, take));

            return result is null ? NotFound() : Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateTag(CreateTagCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddTagProduct(AddTagToProductCommand command)
        {
            var result = await _mediator.Send(command);

            return result?Ok():BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTag(UpdateTagCommand command)
        {
            var result = await _mediator.Send(command);

            return result ?Ok() : BadRequest();
        }
        [HttpPut("untag")]
        public async Task<IActionResult> UnTagProduct(UnTagProductCommand command)
        {
            var result = await _mediator.Send(command);
             
            return result?Ok():BadRequest();
        }
        [HttpDelete("tag/delete")]
        public async Task<IActionResult> RemoveTag(RemoveTagCommand command)
        {
            var result =await _mediator.Send(command);

            return result?Ok("Success!"):BadRequest();
        }
    } 
}
