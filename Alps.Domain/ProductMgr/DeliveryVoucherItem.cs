using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.ProductMgr
{
    public class DeliveryVoucherItem : EntityBase
    {
        public DeliveryVoucher DeliveryVoucher { get; set; }
        public Guid DeliveryVoucherID { get; set; }
        [Display(Name = "物品")]
        public ProductSkuInfo ProductSkuInfo { get; set; }
        [Display(Name = "数量")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        [Display(Name = "重量")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        [Display(Name = "价格")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Display(Name = "金额")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Display(Name = "物品编号")]
        public string ProductNumber { get; set; }

        [Display(Name = "仓库")]
        public Guid PositionID { get; set; }

        [Display(Name = "订单")]
        public Guid? PurchaseOrderItemID { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }

        public virtual Position Position { get; set; }
    }
}
