using System;
using Alps.Domain.Common;
using System.ComponentModel.DataAnnotations;
using Alps.Domain.ProductMgr;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.SaleMgr
{
    public class SaleOrderItem : EntityBase
    {
        public SaleOrder SaleOrder{get;set;}
        public Guid SaleOrderID{get;set;}
        // public virtual Commodity Commodity { get; set; }
        // [Display(Name="品名")]
        // public Guid CommodityID { get; set; }
        public Guid ProductSkuID{get;set;}
        public virtual ProductSku ProductSku{get;set;}
        public string CommodityName{get;set;}

        //public decimal Quantity { get; set; }
        // public Guid UnitID { get; set; }
        [Display(Name="价格")]
         [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }
        public string Remark { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal Quantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal AuxiliaryQuantity{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal Amount{get{return Math.Round(Price*Quantity,1);}}

    }
}
