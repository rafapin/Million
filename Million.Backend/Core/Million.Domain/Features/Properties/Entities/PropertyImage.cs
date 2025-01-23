namespace Million.Domain.Features.Properties.Entities
{
    public class PropertyImage
    {
        public string IdPropertyImage { get; set; } // PK
        public string IdProperty { get; set; } // FK
        public byte[] File { get; set; } = Array.Empty<byte>();
        public bool Enabled { get; set; }

        // Navigation property
        public Property Property { get; set; } = null!;
    }

}
