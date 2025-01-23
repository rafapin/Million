namespace Million.Domain.Features.Shared.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string GenerateId() => Id = string.IsNullOrEmpty(Id) ? Guid.NewGuid().ToString() : Id;
    }
}
