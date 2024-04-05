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
    public class GetInfoShip:IRequest<Ship?>
    {
        public Guid Id { get; set; }

        public GetInfoShip(Guid id)
        {
            Id = id;
        }
    }

    public class HandGetInfoShip : IRequestHandler<GetInfoShip, Ship?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandGetInfoShip(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ship?> Handle(GetInfoShip request, CancellationToken cancellationToken)
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
