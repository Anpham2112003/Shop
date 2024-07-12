using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetOrderShip:IRequest<Ship?>
    {
        public Guid Id { get; set; }

        public GetOrderShip(Guid id)
        {
            Id = id;
        }
    }

    public class HandGetOrderShip : IRequestHandler<GetOrderShip, Ship?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandGetOrderShip(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ship?> Handle(GetOrderShip request, CancellationToken cancellationToken)
        {
            try
            {
                var ship = await _unitOfWork.shipRepository.FindByIdAsync(request.Id);

                return ship;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
