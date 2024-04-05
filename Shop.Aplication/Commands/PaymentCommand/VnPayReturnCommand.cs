using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Aplication.Notify;
using Shop.Domain.Entities;
using Shop.Domain.Enum;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.Services.PaymentService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.PaymentCommand;

public class VnPayReturnCommand:IRequest<VnPayResponseModel>
{
    public string vnp_TmnCode { get; set; }
    
    public string vnp_Amount { get; set; }
    
    public string vnp_BankCode { get; set; }
    
    public string vnp_BankTranNo { get; set; }
    
    public string vnp_CardType { get; set; }
    
    public string vnp_PayDate { get; set; }
    
    public string vnp_OrderInfo { get; set; }
    
    public string vnp_TransactionNo { get; set; }
    
    public string vnp_ResponseCode { get; set; }
    
    public string vnp_TransactionStatus { get; set; }
    
    public string vnp_TxnRef { get; set; }
    
    public string vnp_SecureHash { get; set; }
}


public class HandVnPayReturnCommand:IRequestHandler<VnPayReturnCommand,VnPayResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVnPayService _payService;
    private readonly IConfiguration _configuration;
    private IPublisher _publisher;
    
    public HandVnPayReturnCommand(IUnitOfWork unitOfWork, IVnPayService payService, IConfiguration configuration, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _payService = payService;
        _configuration = configuration;
        _publisher = publisher;
    }

    public async Task<VnPayResponseModel> Handle(VnPayReturnCommand request, CancellationToken cancellationToken)
    {

        try
        {
           var t= request.GetType();
           
            foreach (var p in t.GetProperties())
            {
                _payService.AddResponse(p.Name,p.GetValue(request).ToString());
                
            }
            
            if (!_payService.CheckHash(request.vnp_SecureHash, _configuration["VNPAY:vnp_HashSecret"]))
            {
                return (new VnPayResponseModel()
                {
                    IsSuccess = false,
                    Message = "Giao dịch không hợp lệ!"
                });
            }
            
            var orderId = request.vnp_OrderInfo.Split(':').Last();
            
            if (request.vnp_ResponseCode=="00")
            {
                var order = await _unitOfWork.orderRepository.FindByIdAsync(Guid.Parse(orderId.Trim()));
            
                if (order is null)
                    return new VnPayResponseModel()
                    {
                        IsSuccess = false,
                        Message = " Thanh toán thành công ! Nhưng hệ thống bị lỗi liên hệ Admin để được hỗ trợ !"
                    };
            
                order.OrderState = true;
                order.PaymentMethod = PaymentMethod.VnPay;
                        
                await _unitOfWork.SaveChangesAsync();
                        
                await _unitOfWork.paymentRepository.AddAsync(new Payment()
                {
                    Id = Guid.NewGuid(),
                    PaymentMethod = PaymentMethod.VnPay,
                    Amount = double.Parse(request.vnp_Amount)/100,
                    UserId = order.UserId,
                    BankCode = request.vnp_BankCode,
                    PayDate = DateTime.Now,
                    OrderId = order.Id
                           
                });
            
                await _unitOfWork.SaveChangesAsync();

                await _publisher.Publish(new PaymentNotification(order),cancellationToken);
                       
                return new VnPayResponseModel()
                {
                    IsSuccess = true,
                    Message = "Thanh toán thành công :" + orderId
                };
            }
            
            
            return new VnPayResponseModel()
            {
                IsSuccess = false,
                Message = "Thanh toán không thành công! "
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        

    }
}