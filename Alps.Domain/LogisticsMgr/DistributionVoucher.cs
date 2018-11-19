using Alps.Domain.Common;
using Alps.Domain.SaleMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alps.Domain.LogisticsMgr
{
    public class DistributionVoucher : EntityBase
    {
        public Guid SaleOrderID { get; set; }
        public virtual SaleOrder SaleOrder{get;set;}
        public int Sequence { get; set; }
        public Address DistributionAddress{get;set;}
        public ICollection<DistributionVoucherItem> Items { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Creater { get; set; }
        public DateTime CreatedTime{get;set;}
        public DistributionVoucher()
        {
            this.Items = new HashSet<DistributionVoucherItem>();
            this.DeliveryTime = DateTime.Now;
            this.CreatedTime=DateTime.Now;
        }
        public static DistributionVoucher Create(Guid orderID, string creater)
        {
            DistributionVoucher voucher = new DistributionVoucher();
            voucher.Creater = creater;
            voucher.SaleOrderID = orderID;
            return voucher;
        }
        public void AddItem(Guid saleOrderItemID,Guid productSkuID, decimal auxiliaryQuantity,decimal quantity,decimal price)
        {
            var item=this.Items.FirstOrDefault(p=>p.SaleOrderItemID==saleOrderItemID);
            if(item==null)
            {
                item=new DistributionVoucherItem();
            }
            item.ProductSkuID=productSkuID;
            item.Quantity=quantity;
            item.AuxiliaryQuantity=auxiliaryQuantity;
            item.Price=price;
        }
    }
}
