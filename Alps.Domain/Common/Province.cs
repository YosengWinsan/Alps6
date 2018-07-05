using System;

namespace Alps.Domain
{
    public class Province : EntityBase
    {
        public Guid CountryID { get; set; }
        public virtual Country Country { get; set; }
        public string Name { get; set; }
        public static Province Create(string name,Guid countryID)
        {
            return new Province() { Name = name,CountryID=countryID };
        }
    }
}