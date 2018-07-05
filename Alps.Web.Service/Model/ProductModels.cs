using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
    public class CatagoryListDto
    {
        public string Name { get; set; }
        public Guid ID { get; set; }

    }

    public class ProductListDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        public bool Deprecated { get; set; }
        public bool EnableAuxiliaryQuantity { get; set; }
        public string Catagory { get; set; }


    }
    public class ProductEditDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        public bool Deprecated { get; set; }
        public bool EnableAuxiliaryQuantity { get; set; }
        public Guid CatagoryID { get; set; }
    }

    public class ProductskuListDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Code{get;set;}
        public bool Deleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
    public class ProductskuEditDto
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public Guid ProductID { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public string Code{get;set;}
    }
    public class ProductDetailDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Catagory { get; set; }
        public bool Deprecated { get; set; }
        public IEnumerable<ProductskuListDto> ProductSkuList { get; set; }
    }
}
