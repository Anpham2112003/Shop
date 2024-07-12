
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Queries
{
    public class GetListTag:IRequest<IEnumerable<Tag>>
    {
    }

    public class HandGetListTag : IRequestHandler<GetListTag, IEnumerable<Tag>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandGetListTag(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tag>> Handle(GetListTag request, CancellationToken cancellationToken)
        {
            try
            {
                var tags = await _unitOfWork.tagRepository.GetAllAsync();

                return tags  is null ? Enumerable.Empty<Tag>() : tags;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
