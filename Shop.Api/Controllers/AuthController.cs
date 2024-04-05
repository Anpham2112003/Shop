using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Aplication.Commands;
using Shop.Domain.ResponseModel;


namespace Shop.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController:ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        
        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            this._logger = logger;
            _mediator = mediator;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand user)
        {
            var result = await _mediator.Send(user);
            
            return result is null ? Unauthorized("User or password incorrect!") : Ok(result);

        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(ReFreshTokenCommand tokenCommandToken)
        {
            var result=  await _mediator.Send(tokenCommandToken);

            return result is not null ? Ok(result) : Unauthorized("Token not valid or Expire!");
        }
        
        [HttpGet("verify/account/{token}")]
        public async Task<IActionResult> VerifyAccount(string token)
        {
           var result= await _mediator.Send(new VerifyAccountCommand() { AccountToken = token });
           
           return result ? Ok("success") : BadRequest("Token not valid or Expire!");
        }
        
        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            return result is null ? BadRequest() : Ok(result);
        }
        
        [HttpPost("password/new")]
        public async Task<IActionResult> CreateNewPassword(CreateNewPasswordCommand command)
        {
            var result= await _mediator.Send(command);
            
            return result ? Ok("success") : BadRequest();
        }
        
        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
           var result= await _mediator.Send(command);

           return result ? Ok("success") : BadRequest();
        }
        

    }
}
