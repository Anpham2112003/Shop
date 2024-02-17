using Microsoft.AspNetCore.Mvc;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Api.Controller
{
    public class TestController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/test")]
        public async Task<IActionResult> Testd()
        {
          var Result=  await _unitOfWork.userRepository.Count();
            return Ok(Result);
        }
    }
}
