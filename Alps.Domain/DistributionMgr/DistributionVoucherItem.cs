using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alps.Domain.Common;
using Alps.Domain.ProductMgr;
using Alps.Domain.SaleMgr;
namespace Alps.Domain.DistributionMgr
{
    public class DistributionVoucherItem : EntityBase
    {
        public Guid SaleOrderItemID{get;set;}
       // public Commodity Commodity { get; set; }
        public Guid ProductSkuID { get; set; }
        public virtual ProductSku ProductSku{get;set;}
        public decimal AuxiliaryQuantity{get;set;}
        public decimal Quantity{get;set;}
        public decimal Price{get;set;}
        
    }
}
