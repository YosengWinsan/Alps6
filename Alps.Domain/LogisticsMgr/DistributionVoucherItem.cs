using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alps.Domain.Common;
using Alps.Domain.ProductMgr;
using Alps.Domain.SaleMgr;
namespace Alps.Domain.LogisticsMgr
{
    public class DistributionVoucherItem : EntityBase
    {
        public Guid SaleOrderItemID{get;set;}
       // public Commodity Commodity { get; set; }
        public Guid ProductSkuID { get; set; }
        public virtual ProductSku ProductSku{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal AuxiliaryQuantity{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal Quantity{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal Price{get;set;}
        
    }
}
