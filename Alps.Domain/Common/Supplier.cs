

using System;
using Alps.Domain.PurchaseMgr;

namespace Alps.Domain.Common
{
    public class Supplier:EntityBase 
    {
        public string Name { get; set; }
        public Address Address{get;set;}
        public Guid SupplierClassID{get;set;}
        public virtual SupplierClass SupplierClass{get;set;}
        public static Supplier Create(string name,Guid supplierClassID,Address address)
        {
            return new Supplier() { Name = name ,Address=address,SupplierClassID=supplierClassID};
        }
    }
}
