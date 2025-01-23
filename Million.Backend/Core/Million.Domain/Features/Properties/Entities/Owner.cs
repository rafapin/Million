namespace Million.Domain.Features.Properties.Entities
{
    public class Owner
    {
        public Guid IdOwner { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public DateTime Birthday { get; set; }

        // Navigation property
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }

}
