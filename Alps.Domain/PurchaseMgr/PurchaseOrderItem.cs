using System;
using Alps.Domain.ProductMgr;
using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.PurchaseMgr
{
    public class PurchaseOrderItem : EntityBase
    {
        public PurchaseOrder PurchaseOrder { get; set; }
        public Guid PurchaseOrderID { get; set; }
        //[Display(Name = "品名")]
        //public Guid ProductID { get; set; }

        public ProductSkuInfo ProductSkuInfo { get; set; }
        [Display(Name = "数量")]
        public decimal Quantity { get; set; }
        [Display(Name = "重量")]
        public decimal Weight { get; set; }
        //[Display(Name="单位")]
        //public Guid UnitID { get; set; }
        //[Display(Name = "计价数量")]
        //public decimal PricingQuantity { get; set; }
        //public Guid PricingUnitID { get; set; }
        //[Display(Name = "计价方式")]
        //public PricingMethod PricingMethod { get; set; }
        [Display(Name = "单价")]
        public decimal Price { get; set; }
        [Display(Name = "金额")]
        public decimal Amount { get; set; }

        //public virtual Unit Unit { get; set; }
        public virtual Product Product { get; set; }
        //public virtual Unit PricingUnitID { get; set; }
    }
    public class ProductInOrder
    {
        [Display(Name = "品名")]
        public Guid ProductID { get; set; }
        [Display(Name = "品名")]
        public string Name { get; set; }
        [Display(Name = "计价方式")]
        public PricingMethod PricingMethod { get; set; }
        public ProductInOrder() { }
        public ProductInOrder(Guid id) : this(id, string.Empty) { }
        public ProductInOrder(Guid id, string name)
            : this(id, name, PricingMethod.PricingByQuantity)
        {
        }
        public ProductInOrder(Guid id, string name, PricingMethod pricingMethod)
        {
            this.ProductID = id;
            this.Name = name;
            this.PricingMethod = pricingMethod;
        }
    }
}
