using AutoMapper;
using MediatR;
using Million.Application.Features.Properties.ViewModels;
using Million.Domain.Contracts;
using Million.Domain.Features.Properties.Entities;

namespace Million.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQueryHandler 
        (IGenericRepository<Property> repository, IMapper mapper)
        : IRequestHandler<GetPropertyByIdQuery, PropertyDetailVm>
    {
        public async Task<PropertyDetailVm> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await repository.GetByIdAsync(request.Id);
            var resull = mapper.Map<PropertyDetailVm>(property);
            return resull;
        }
    }
}
