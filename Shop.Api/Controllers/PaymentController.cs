using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Aplication.Commands.PaymentCommand;
using Shop.Aplication.Queries;
using Shop.Domain.Options;
using Shop.Domain.ResponseModel;
using VNPAY_CS_ASPX;

namespace Shop.Api.Controllers;
[ApiController]
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

    [Authorize]
    [HttpGet("payments")]
    public async Task<IActionResult> GetPayment( [FromQuery] int page ,int take)
    {
        var result = await _mediator.Send(new GetHistoryPayment(page, take));

        return result is null ?  NotFound() : Ok(result);
    }

    [Authorize]
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