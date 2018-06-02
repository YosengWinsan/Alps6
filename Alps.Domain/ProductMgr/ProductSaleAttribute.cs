using System;
using Alps.Domain.Common;
namespace Alps.Domain.ProductMgr
{
    public class ProductSaleAttribute:EntityBase
    {
        public string Name { get; set; }
        public Guid CatagoryID { get; set; }
        public string TextPrompt { get; set; }
        public bool IsRequired{get;set;}
        public AttributeControlType AttributeControlType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
