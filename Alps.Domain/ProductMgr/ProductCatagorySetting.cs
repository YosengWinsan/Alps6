using System;

namespace Alps.Domain.ProductMgr
{
    public class ProductCatagorySetting:EntityBase
    {
        public Guid ProductID { get; set; }
        public Guid CatagoryID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        
        public static ProductCatagorySetting Create(Guid productID,Guid catagoryID,string name,int displayOrder)
        {
            return new ProductCatagorySetting(){ProductID=productID, CatagoryID=catagoryID,Name=name,DisplayOrder=displayOrder};
        }
    }
}
