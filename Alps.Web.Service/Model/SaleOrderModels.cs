using System;
using System.Collections.Generic;
using Alps.Domain.SaleMgr;

namespace Alps.Web.Service.Model
{
    public class SaleOrderListDto
    {
        public Guid ID { get; set; }
        public string Customer { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAuxiliaryQuantity { get; set; }
        public decimal TotalAmount { get; set; }

    }
    public class SaleOrderItemEditDto:ISaleOrderItem
    {
        public Guid ID{get;set;}
        public Guid ProductSkuID{get;set;}
       // public string ProductSkuName{get;set;}
        public string CommodityName{get;set;}
        public decimal Quantity{get;set;}
        public decimal AuxiliaryQuantity{get;set;}
        public decimal Price{get;set;}
        public string  Remark{get;set;}
        public decimal Amount{get;set;}
    }
    public class SaleOrderEditDto
    {

        public Guid CustomerID{get;set;}
        public Guid ID{get;set;} 
        public IEnumerable<SaleOrderItemEditDto> Items{get;set;}

    }
    public class SaleOrderDetailDto
    {
        public Guid ID{get;set;}
        public string Customer{get;set;}
        public string Status{get;set;}
        public int StatusValue{get;set;}
        public decimal TotalQuantity{get;set;}
        public decimal TotalAuxiliaryQuantity{get;set;}
        public decimal TotalAmount{get;set;}
        public IEnumerable<SaleOrderItemEditDto> Items{get;set;}
    }
}