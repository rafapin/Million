using Million.Domain.Features.Shared.Entities;

namespace Million.Domain.Features.Properties.Entities
{
    public class Property: Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }

        // Foreign Key
        public string IdOwner { get; set; }

        // Navigation properties
        public Owner Owner { get; set; } = null!;
        public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();
        public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
    }


}
