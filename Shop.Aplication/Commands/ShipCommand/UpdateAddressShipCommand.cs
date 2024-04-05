using MediatR;
using Shop.Domain.Enums;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.ShipCommand
{
    public class UpdateAddressShipCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
    }

    public class HandUpdateAddressShipCommand : IRequestHandler<UpdateAddressShipCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandUpdateAddressShipCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateAddressShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ship = await _unitOfWork.shipRepository.FindByIdAsync(request.Id);

                var addsress = await _unitOfWork.addressRepository.FindByIdAsync(request.AddressId);

                if (ship == null || ship.State != ShipState.Waiting || addsress == null) return false;

                ship.StreetAddress = addsress.StreetAddress;
                ship.City = addsress.City;
                ship.Commune = addsress.Commune;
                ship.District = addsress.District;

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
   
                throw;
            }
            
        }
    }
}
