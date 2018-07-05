using System;

namespace Alps.Domain
{
    public class County : EntityBase
    {
        public Guid CityID { get; set; }
        public City City { get; set; }
         public string Name { get; set; }
        public static County Create(string name,Guid cityID)
        {
            return new County() { Name = name,CityID=cityID };
        }
    }
}