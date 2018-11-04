using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.LogisticsMgr
{
    public enum DispatchType
    {
        [Display(Name = "进")]
        In,
        [Display(Name = "出")] 
        Out
    }
}
