using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alps.Domain.Common;
using Alps.Domain.SaleMgr;
namespace Alps.Domain.DistributionMgr
{
    public class DistributionVoucherItem : EntityBase
    {
        public Commodity Commodity { get; set; }
        public Guid CommodityID { get; set; }
        public Quantity Quantity { get; set; }
    }
}
