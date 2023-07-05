

namespace BackupManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DestinationProviderAttribute : Attribute
    {
        public string Name { get; set; }

        public DestinationProviderAttribute(string name)
        {
            Name = name;
        }

    }
}
