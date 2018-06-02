
namespace Alps.Domain.Common
{
    public class Department:EntityBase
    {
        public string Name { get; set; }
        public static Department Create(string name)
        {
            Department d = new Department();
            d.Name = name;
            return d;
        }
    }
}
