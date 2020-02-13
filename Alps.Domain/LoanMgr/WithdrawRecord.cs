using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.LoanMgr
{
    public class WithdrawRecord:EntityBase
    {
        public Guid LoanVoucherID{get;set;}
        public LoanVoucher LoanVoucher{get;set;}
        public DateTimeOffset Date{get;set;}
        
        public DateTimeOffset DepositDate{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal Amount{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal Interest{get;set;}
         [Column(TypeName="decimal(18,2)")]
        public decimal InterestRate{get;set;}
        public string Remark{get;set;}
        public string Operator{get;set;}
        public DateTimeOffset ModifyDate{get;set;}
        public WithdrawRecord()
        {
            this.ModifyDate=DateTime.Now;
        }
    }
    
}