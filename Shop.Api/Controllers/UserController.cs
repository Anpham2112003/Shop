using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.Commands;
using Shop.Aplication.Notify;
using Shop.Aplication.Queries;
using Shop.Aplication.Queries.UserQueries;
using Shop.Domain.ResponseModel;

namespace Shop.Api.Controllers
{
    [Route("api/user")]
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
            var user = await _mediator.Send(new GetInfoUserById(id));

            return user is null ? NotFound("Not found user") : Ok(user);
        }

        [Authorize]
        [HttpGet("all/{page:int:min(1)}/{take:int:min(1)}")]
        public async Task<IActionResult> GetAllUser(int page, int take)
        {
            var result =await _mediator.Send(new GetAllUser(page, take));
            
            return result is not null ? Ok(result) : NotFound("Not found user");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand user)
        {
           var result=  await  _mediator.Send(user);

           return result ? Ok(user) : BadRequest("User  Exist ! ");
        }



        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateUserById([FromBody] UpdateUserCommand user)
        {
            
            var result = await _mediator.Send(user);

            return result ? Ok(result) : BadRequest("Not found Userid =" + user.Id);
        }



        [Authorize]        
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return result ? Ok(result) : BadRequest("Not found UserId ="+id);
        }
        
       
    }
}
