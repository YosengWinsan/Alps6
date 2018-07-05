using System;

namespace Alps.Domain{

    public class Address
    {
        public Guid CountyID{get;set;}
        public virtual County County{get;set;}
        public string Street{get;set;}
        public static Address Create(County county,string street)
        {
            return new Address(){Street=street,CountyID=county.ID};
        }
    }
}