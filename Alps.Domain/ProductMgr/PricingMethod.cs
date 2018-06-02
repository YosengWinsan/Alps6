using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.ProductMgr
{
    public enum PricingMethod
    {
        [Display(Name = "按件数")]
        PricingByQuantity=0,
        [Display(Name = "按重量")]
        PricingByWeight
    }
}
