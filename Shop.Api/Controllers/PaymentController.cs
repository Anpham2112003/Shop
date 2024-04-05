using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Aplication.Commands.PaymentCommand;
using Shop.Domain.Options;
using Shop.Domain.ResponseModel;
using VNPAY_CS_ASPX;

namespace Shop.Api.Controllers;

[Route("api")]
public class PaymentController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOptions<VnPayOptions> _options;


    public PaymentController(IMediator mediator, IOptions<VnPayOptions> options)
    {
        _mediator = mediator;
        _options = options;
    }

    [HttpPost("payment/VnPay")]
    public async Task<IActionResult> PaymentVnPay(PaymentCommand command)
    {
       
        var result = await _mediator.Send(new VnPayCommand(command.Id,Request.HttpContext));

        return result is null ? BadRequest("Not found!") : Ok(result);

    }

    [HttpGet("payment/VnPay/return")]
    public async Task<IActionResult> HandResultVnPay([FromQuery] VnPayReturnCommand command)
    {
       var result=  await _mediator.Send(command);
       return Ok(result);
    }


    
}