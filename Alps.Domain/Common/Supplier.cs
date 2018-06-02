

namespace Alps.Domain.Common
{
    public class Supplier:EntityBase 
    {
        public string Name { get; set; }
        public static Supplier Create(string name)
        {
            return new Supplier() { Name = name };
        }
    }
}
