﻿using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Enums;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ShipCommnd;

public class CreateShipCommand:IRequest<bool>
{
    public Guid Id =Guid.NewGuid();
    
    public Guid OrderId { get; set; }

    
    public Guid AddressId { get; set; }
    
}

public class HandCreateShipCommand:IRequestHandler<CreateShipCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandCreateShipCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateShipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _unitOfWork.addressRepository.FindByIdAsync(request.AddressId);

            var order = await _unitOfWork.orderRepository.FindByIdAsync(request.OrderId);
            
            if (address is null||order is null) return false;

            var ship = new Ship()
            {
                Id = request.Id,
                OrderId = order.Id,
                State=ShipState.Waiting,
                StreetAddress = address.StreetAddress,
                District = address.District,
                Commune = address.Commune,
                City = address.City,
                
            };

            await _unitOfWork.shipRepository.AddAsync(ship);

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        catch (Exception )
        {

            throw ;
        }
       
        
        
    }
}