namespace cTime.Core.Data
{
    public class License
    {
        public string Name { get; }

        public License(string name)
        {
            Guard.NotNull(name, nameof(name));

            this.Name = name;
        }
    }
}