using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.SaleMgr
{
    public enum SaleOrderStatus 
    {
        [Display(Name = "未提交")]
        UnConfirm=0,
        [Display(Name = "已提交")]
        Confirm
    }
}
