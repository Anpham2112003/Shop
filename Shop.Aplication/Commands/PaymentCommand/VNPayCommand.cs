using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Infratructure.Services.PaymentService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.PaymentCommand;

public class VnPayCommand:IRequest<string?>
{
    public Guid Id { get; set; }
    public HttpContext Context { get; }

    public VnPayCommand(Guid id, HttpContext context)
    {
        Id = id;
        Context = context;
    }
}

public class HandVnPayCommand:IRequestHandler<VnPayCommand,string?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVnPayService _payService;

    public HandVnPayCommand(IUnitOfWork unitOfWork, IVnPayService payService)
    {
        _unitOfWork = unitOfWork;
        _payService = payService;
    }

    public async Task<string?> Handle(VnPayCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            var order = await _unitOfWork.orderRepository.FindByIdAsync(request.Id);

            if (order is null) return null;

            return _payService.GenerateUrl(order.TotalPrice, 
                "Thanh toan hoa don:" + order.Id.ToString(),
                request.Context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}

