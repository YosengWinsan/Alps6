

namespace Alps.Domain.Common
{
    public class Supplier:EntityBase 
    {
        public string Name { get; set; }
        public Address Address{get;set;}
        public static Supplier Create(string name,Address address)
        {
            return new Supplier() { Name = name ,Address=address};
        }
    }
}
