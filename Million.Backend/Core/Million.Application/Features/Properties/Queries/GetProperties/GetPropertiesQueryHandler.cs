using AutoMapper;
using MediatR;
using Million.Application.Features.Properties.ViewModels;
using Million.Domain.Contracts;
using Million.Domain.Features.Properties.Entities;

namespace Million.Application.Features.Properties.Queries.GetProperties
{
    public class GetPropertiesQueryHandler 
        (IGenericRepository<Property> repository, IMapper mapper)
        : IRequestHandler<GetPropertiesQuery, List<PropertyVm>>
    {
        public async Task<List<PropertyVm>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await repository.GetAsync(property=> 
                (string.IsNullOrEmpty(request.Name) ||  property.Name.ToLower().Contains(request.Name.ToLower() )
                && (string.IsNullOrEmpty(request.Address) ||  property.Address.ToLower().Contains(request.Address.ToLower() ))
                ));
            return mapper.Map<List<PropertyVm>>(properties);
        }
    }
}
