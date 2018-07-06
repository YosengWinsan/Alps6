using System;

namespace Alps.Domain
{
    public class County : EntityBase
    {
        public Guid CityID { get; set; }
        public virtual City City { get; set; }
         public string Name { get; set; }
         public string FullName{get;set;}
        public static County Create(string name,Guid cityID)
        {
            return new County() { Name = name,CityID=cityID };
        }
        public static County Create(string name,City city)
        {
            return new County(){Name=name,FullName=city.Name+name,CityID=city.ID};
        }
    }
}