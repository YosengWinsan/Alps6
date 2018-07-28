
using System;

namespace Alps.Web.Service.Model
{
    public class CommodityListDto
    {
        public Guid ID { get; set; }
        public string CommodityName { get; set; }
        public decimal QuantityRate { get; set; }
        public decimal PreSellQuantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal PreSellAuxiliaryQuantity { get; set; }
        public decimal StockQuantity{get;set;}
        public decimal StockAuxiliaryQuantity{get;set;}
        public decimal OrderedAuxiliaryQuantity{get;set;}
        public decimal OrderedQuantity{get;set;}
        public decimal SellableAuxiliaryQuantity{get;set;}
        public decimal SellableQuantity{get;set;}
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