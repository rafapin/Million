using MediatR;
using Million.Application.Features.Properties.ViewModels;

namespace Million.Application.Features.Properties.Queries.GetProperties
{
    public class GetPropertiesQuery: IRequest<List<PropertyVm>>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
