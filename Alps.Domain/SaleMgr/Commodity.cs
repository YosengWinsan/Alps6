using System;
using Alps.Domain.Common;

using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.SaleMgr
{
    public class Commodity : EntityBase
    {


        [Display(Name = "商品名")]
        public string Name { get; set; }
        [Display(Name = "商品描述")]
        public string Description { get; set; }
        public Guid ProductSkuID { get; set; }
        [Display(Name = "定价")]
        public decimal ListPrice { get; set; }
        [Display(Name = "库存数量")]
        public decimal StockQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal SellableQuantity { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal StockAuxiliaryQuantity { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal OrderedAuxiliaryQuantity { get; set; }
        public decimal SellableAuxiliaryQuantity { get; set; }

        [Display(Name = "期货否")]
        public bool IsFutures { get; set; }
        [Display(Name = "交货日期")]
        public DateTime? DateOfDelivery { get; set; }
        public bool IsVirtualCommodity { get; set; }
        public decimal QuantityRate { get; set; }

        public static Commodity Create(Guid productSkuID, string name, string description, decimal listPrice, decimal quantity, decimal auxiliaryQuantity)
        {
            return new Commodity() { ProductSkuID = productSkuID, Name = name, Description = description, ListPrice = listPrice, Quantity = quantity, AuxiliaryQuantity = auxiliaryQuantity 
            ,QuantityRate=2.5m,IsVirtualCommodity=false
            };
        }

    }
}
