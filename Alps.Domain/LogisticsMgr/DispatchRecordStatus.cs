using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.LogisticsMgr
{
   public enum DispatchRecordStatus{
       [Display(Name="正常")]   
        Normal,
        [Display(Name="进行中")]   
       InProcess,
       [Display(Name="完成")]   
       Completed,
       [Display(Name="作废")]   
       Cancelled
} 
}
