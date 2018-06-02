using Alps.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alps.Domain.DistributionMgr
{
    public class DistributionVoucher : EntityBase
    {
        public Guid OrderID { get; set; }
        public int Sequence { get; set; }
        public ICollection<DistributionVoucherItem> Items { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Creater { get; set; }
        public DistributionVoucher()
        {
            this.Items = new HashSet<DistributionVoucherItem>();
            this.DeliveryTime = DateTime.Now;
        }
        public static DistributionVoucher Create(Guid orderID, string creater)
        {
            DistributionVoucher voucher = new DistributionVoucher();
            voucher.Creater = creater;
            voucher.OrderID = orderID;
            return voucher;
        }
        public void AddItem(Guid commodityID, Quantity quantity)
        {
            var item=this.Items.FirstOrDefault(p=>p.CommodityID==commodityID);
            if(item==null)
            {
                item=new DistributionVoucherItem();
            }
            item.CommodityID=commodityID;
            item.Quantity=quantity;
        }
    }
}
