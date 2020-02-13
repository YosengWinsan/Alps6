using System;
using Alps.Domain.Common;

using System.ComponentModel.DataAnnotations;
using Alps.Domain.ProductMgr;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.SaleMgr
{
    public class Commodity : EntityBase
    {
        [Display(Name = "商品名")]
        public string Name { get; set; }
        [Display(Name = "商品描述")]
        public string Description { get; set; }
        public Guid ProductSkuID { get; set; }
        
        //public  ProductSku ProductSku{get;set;}

        public ProductSku Sku{get;set;}
        // public Guid OwnerID{get;set;}
        // public Department Owner{get;set;       
        // }
        [Display(Name = "定价")]
         [Column(TypeName="decimal(18,2)")]
        public decimal ListPrice { get; set; }
        [Display(Name = "库存数量")]
         [Column(TypeName="decimal(18,2)")]
        public decimal StockQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal PreSellQuantity { get; set; } 
        public decimal SellableQuantity { get{return PreSellQuantity+StockQuantity-OrderedQuantity;}  }
         [Column(TypeName="decimal(18,2)")]
        public decimal OrderedQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal StockAuxiliaryQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal PreSellAuxiliaryQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal OrderedAuxiliaryQuantity { get; set; }
        public decimal SellableAuxiliaryQuantity { get{return PreSellAuxiliaryQuantity+StockAuxiliaryQuantity-OrderedAuxiliaryQuantity;} }


        [Display(Name = "期货否")]
        public bool IsFutures { get; set; }
        [Display(Name = "交货日期")]
        public DateTime? DateOfDelivery { get; set; }
        public bool IsVirtualCommodity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal QuantityRate { get; set; }

        public static Commodity Create(Guid productSkuID, string name, string description, decimal listPrice, decimal quantity, decimal auxiliaryQuantity)
        {
            return new Commodity() {ID=productSkuID, ProductSkuID = productSkuID, Name = name, Description = description, ListPrice = listPrice, PreSellQuantity = quantity, PreSellAuxiliaryQuantity = auxiliaryQuantity 
            ,QuantityRate=2.5m,IsVirtualCommodity=false
            };
        }
    }
}
