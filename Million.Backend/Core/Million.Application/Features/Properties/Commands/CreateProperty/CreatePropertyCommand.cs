using MediatR;
using Million.Domain.Features.Shared.Entities;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public string IdOwner { get; set; }
        public CustomFile? Image { get; set; }
    }
}
