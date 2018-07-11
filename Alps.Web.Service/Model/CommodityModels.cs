
using System;

namespace Alps.Web.Service.Model
{
    public class CommodityListDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public Guid ProductSkuID { get; set; }
    }
    public class CommodityEditDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal ListPrice { get; set; }
        public Guid ProductSkuID { get; set; }
    }
    
}