using System;
using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.ProductMgr
{
    public class WarehouseVoucherItem : EntityBase
    {
        public WarehouseVoucher WarehouseVoucher { get; set; }
        public Guid WarehouseVoucherID { get; set; }
        public ProductSkuInfo ProductSkuInfo { get; set; }
        [Display(Name = "数量")]
        public decimal Quantity { get; set; }
        [Display(Name ="重量")]
        public decimal Weight { get; set; }
        [Display(Name = "辅助数量")]
        public decimal AuxiliaryQuantity { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "金额")]
        public decimal Amount { get; set; }
        [Display(Name = "物品编号")]
        public string ProductNumber { get; set; }

        [Display(Name = "仓库")]
        public Guid PositionID { get; set; }

        [Display(Name = "订单")]
        public Guid? PurchaseOrderItemID { get; set; }
        [Display(Name="备注")]
        public string Remark { get; set; }
        [Display(Name = "运费")]
        public decimal Freight { get; set; }
        [Display(Name = "税收")]
        public decimal Tax { get; set; }
        //[Display(Name = "含税单价")]
        //public decimal PriceIncludingTax { get; set; }
        //[Display(Name = "含税金额")]
        //public decimal AmountIncludingTax { get; set; }
        //[Display(Name="总金额")]
        //public decimal TotalAmount { get; set; }
        //public virtual Unit Unit { get; set; }
        public virtual Position Position { get; set; }
        
        //public virtual Product Product { get; set; }
    }
    
}
