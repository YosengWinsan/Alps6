namespace Alps.Domain
{
    public class Country : EntityBase
    {
        public string Name { get; set; }
        public static Country Create(string name)
        {
            return new Country() { Name = name };
        }
    }
}