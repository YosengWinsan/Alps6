using System;

namespace Alps.Domain
{
    public class City:EntityBase{
        public Guid ProvinceID { get; set; }
        public virtual Province Province { get; set; }
        public string Name { get; set; }
        public static City Create(string name,Guid provinceID)
        {
            return new City() { Name = name,ProvinceID=provinceID };
        }
    }
}