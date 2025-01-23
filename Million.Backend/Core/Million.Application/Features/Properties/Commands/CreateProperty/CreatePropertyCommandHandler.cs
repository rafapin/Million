using AutoMapper;
using MediatR;
using Million.Domain.Contracts;
using Million.Domain.Features.Properties.Entities;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler
        (IGenericRepository<Property> repository, IMapper mapper)
        : IRequestHandler<CreatePropertyCommand, bool>
    {
        public async Task<bool> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = mapper.Map<Property>(request);
            property.GenerateId();
            foreach(var image in property.PropertyImages)
            {
                image.IdPropertyImage = Guid.NewGuid().ToString();
                image.IdProperty = property.Id;
            }
            await repository.AddAsync(property);
            return true;
        }
    }
}
