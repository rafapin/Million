namespace Million.Domain.Features.Shared.Entities
{
    public class CustomFile
    {
        public CustomFile(byte[] file, string name, string? type = null, double? length = null)
        {
            File = file;
            var partsName = name.Split('.');
            Name = partsName.First();
            Extension = partsName.Last();
            Type = type;
            SizeBytes = length;
        }

        public byte[] File { get; private set; }
        public string Name { get; private set; }
        public string FullName => $"{Name}.{Extension}";
        public string Extension { get; private set; }
        public string? Type { get; private set; }
        public double? SizeBytes { get; private set; }

        public void ModifyName(string newName)
        {
            Name = newName;
        }

        public void GenerateRandonName() => ModifyName(Guid.NewGuid().ToString());

    }
}
