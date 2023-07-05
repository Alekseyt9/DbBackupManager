

namespace BackupManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SourceProviderAttribute : Attribute
    {
        public string Name { get; set; }

        public SourceProviderAttribute(string name)
        {
            Name = name;
        }

    }
}
