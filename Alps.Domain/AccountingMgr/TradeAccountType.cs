
using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.AccountingMgr
{
    public enum TradeAccountType:int
    {
        [Display(Name="供应商")]
        Supplier,
        [Display(Name = "客户")]
        Customer,
        [Display(Name = "供应商和客户")]
        SupplierAndCustomer
    }
}
