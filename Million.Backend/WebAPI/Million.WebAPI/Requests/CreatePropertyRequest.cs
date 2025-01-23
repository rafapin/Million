using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.WebAPI.Extensions;

namespace Million.WebAPI.Requests
{
    public class CreatePropertyRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }
        public string? IdOwner { get; set; }
        public IFormFile? Image { get; set; }

        public async Task<CreatePropertyCommand> ToCommand()
        {
            return new CreatePropertyCommand
            {
                Name = this.Name!,
                Address = this.Address!,
                Price = this.Price,
                CodeInternal = this.CodeInternal!,
                Year = this.Year,
                IdOwner = this.IdOwner!,
                Image = await this.Image!.ToCustomFile()
            };
        }
    }
}
