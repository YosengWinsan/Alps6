using System;

namespace Alps.Domain.LoanMgr
{
    public class WithdrawRecord:EntityBase
    {
        public Guid LoanVoucherID{get;set;}
        public LoanVoucher LoanVoucher{get;set;}
        public DateTimeOffset Date{get;set;}
        
        public DateTimeOffset DepositDate{get;set;}
        public decimal Amount{get;set;}
        public decimal Interest{get;set;}
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