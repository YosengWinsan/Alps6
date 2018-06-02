using System;
using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.ProductMgr
{
    public class MaterialRequisitionItem:EntityBase
    {
        public MaterialRequisition MaterialRequisition { get; set; }
        public Guid MaterialRequisitionID { get; set; }
        [Display(Name="物品")]
        public ProductSkuInfo ProductSkuInfo { get; set; }
        [Display(Name = "数量")]
        public decimal Quantity { get; set; }
        //[Display(Name = "单位")]
        //public Guid UnitID { get; set; }
        [Display(Name = "重量")]
        public decimal Weight { get; set; }
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "金额")]
        public decimal Amount { get; set; }
        [Display(Name = "物品编号")]
        public string ProductNumber { get; set; }

        [Display(Name = "仓库")]
        public Guid PositionID { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
