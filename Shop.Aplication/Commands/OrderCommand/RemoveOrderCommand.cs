using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Enums;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.OrderCommand
{
    public class RemoveOrderCommand:IRequest<Guid?>
    {
        public Guid OrderId { get; set; }
    }

    public class HandRemoveOrderCommand : IRequestHandler<RemoveOrderCommand, Guid?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public HandRemoveOrderCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<Guid?> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.StartTransation();
            try
            {
                var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

                var order = await _unitOfWork.orderRepository.FindByIdAsync(request.OrderId);

                var ship = _unitOfWork.shipRepository.FindWhere(x => x.OrderId == request.OrderId).FirstOrDefault();

                if (order is null  || ship is null || ship.State == ShipState.Shipping || ship.State == ShipState.Success) return null;

                order.IsDeleted = true;
                order.DeletedAt = DateTime.UtcNow;

                _unitOfWork.orderRepository.Update(order);

                _unitOfWork.shipRepository.Remove(ship);

                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();

                return order.Id;

                
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
