using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Api.Auth;

namespace Shop.Api.Controllers
{
    [Route("api")]
    public class AuthController:ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly ISender _sender;
        private readonly IOptionsMonitor<JwtOptions> options;

        
        public AuthController(ILogger<AuthController> logger, ISender sender, IOptionsMonitor<JwtOptions> options)
        {
            this.logger = logger;
            _sender = sender;
            this.options = options;
        }

        public Task<IActionResult> Login()
        {

        }


    }
}
