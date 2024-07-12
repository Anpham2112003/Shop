using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands;
using Shop.Aplication.Commands.AuthCommand;
using Shop.Aplication.Commands.UserCommand;
using Shop.Aplication.Queries;
using Shop.Domain.ResponseModel;

namespace Shop.Api.Controllers
{
    [ApiController]
    [Route("api/user/profile")]
    public class UserController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
       

        public UserController(ILogger<UserController> logger,  IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _mediator.Send(new GetProfileUser(id));

            return user is null ? NotFound("Not found user") : Ok(user);
        }

       

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateUserById([FromBody] UpdateUserCommand user)
        {
            
            var result = await _mediator.Send(user);

            return result ? Ok(result) : BadRequest("Not found ");
        }

        [Authorize]
        [HttpPatch("avatar/edit")]
        public async Task<IActionResult> UpdateAvatar([FromForm]UpdateAvatarUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("avatar/delete")]
        public async Task<IActionResult> RemoveAvatar()
        {
            var result = await _mediator.Send(new RemoveAvatarUserCommand());

            return result ? Ok(result) : BadRequest();
        }


        
        
       
    }
}
