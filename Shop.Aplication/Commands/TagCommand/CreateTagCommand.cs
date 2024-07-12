using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Commands.TagCommand
{
    public class CreateTagCommand:IRequest<Tag>
    {
        public string? Name {  get; set; }
    }

    public class HandCreateTagCommand : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandCreateTagCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    TagName = request.Name,
                };
                await _unitOfWork.tagRepository.AddAsync(tag);

                await _unitOfWork.SaveChangesAsync();

                return tag;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
