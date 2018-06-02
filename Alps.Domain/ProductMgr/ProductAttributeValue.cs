using System;

namespace Alps.Domain.ProductMgr
{
    public class ProductAttributeValue:EntityBase
    {
        public Guid ProductAttributeID { get; set; }
        public Guid CatagoryID { get; set; }
        public string Name { get; set; }
    }
}
