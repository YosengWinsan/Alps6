using System;
using Alps.Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.SaleMgr
{
    public class SaleOrderItem : EntityBase
    {
        public SaleOrder SaleOrder{get;set;}
        public Guid SaleOrderID{get;set;}
        public Commodity Commodity { get; set; }
        [Display(Name="品名")]
        public Guid CommodityID { get; set; }
        //public decimal Quantity { get; set; }
        // public Guid UnitID { get; set; }
        [Display(Name="价格")]
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuxiliaryQuantity{get;set;}
        public decimal Amount{get{return Math.Round(Price*Quantity,1);}}
        public void UpdateAmount(){
            //this.Amount=this.Quantity*this.Price;
        }
    }
}
