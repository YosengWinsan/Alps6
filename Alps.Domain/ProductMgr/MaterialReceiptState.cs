using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.ProductMgr
{
    public enum MaterialReceiptState
    {
        [Display(Name = "未确认")]
        Unconfirmed,
        [Display(Name = "已确认")]
        Confirmed
    }
}
