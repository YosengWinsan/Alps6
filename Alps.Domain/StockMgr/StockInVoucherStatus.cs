using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.StockMgr
{
    public enum StockInVoucherStatus
    {
        [Display(Name = "未提交")]
        Unsubmit,
        [Display(Name = "审核中")]
        UnderReview,
        [Display(Name = "已确认")]
        Confirm,
        [Display(Name ="已删除")]
        Deleted
            
    }
}
