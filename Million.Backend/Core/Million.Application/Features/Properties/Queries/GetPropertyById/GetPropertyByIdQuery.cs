using MediatR;
using Million.Application.Features.Properties.ViewModels;

namespace Million.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery: IRequest<PropertyDetailVm>
    {
        public string Id { get; set; } = string.Empty;
    }
}
