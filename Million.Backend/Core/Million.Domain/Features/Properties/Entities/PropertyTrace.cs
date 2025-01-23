namespace Million.Domain.Features.Properties.Entities
{
    public class PropertyTrace
    {
        public Guid IdPropertyTrace { get; set; } // PK
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }

        // Foreign Key
        public Guid IdProperty { get; set; }

        // Navigation property
        public Property Property { get; set; } = null!;
    }


}
