using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Extentions;
using Shop.Aplication.ResultOrError;
using Shop.Domain.RequestModel;

namespace Shop.Api.Controllers
{
    [Route("api")]
    public class UserController:ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ISender _sender;

        public UserController(ILogger<UserController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost("user/create")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand user)
        {
           var result=  await  _sender.Send(user);
          
          return this.ResultOrError<CreateUserCommand>( result );
        }
    }
}
