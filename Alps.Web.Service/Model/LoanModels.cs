using System;

namespace Alps.Web.Service.Model
{
    public class LoanVoucherListDto
    {
        public Guid ID { get; set; }
        public string Lender { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public bool InterestSettlable { get; set; }
    }
    public class DepositDto
    {
         public Guid LenderID { get; set; }
        public decimal Amount { get; set; }
        public string VoucherNumber { get; set; }
        public DateTimeOffset Date { get; set; }
    }
    public class WithdrawDto
    {
        public Guid LoanVoucherID { get; set; }
        public decimal Amount { get; set; }
    }
    public class WaterBillDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Interest { get; set; }
        public OperateType Type { get; set; }

    }
    public class LoanVoucherInfoDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset InterestSettlementDate { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int InterestDays { get; set; }
        public int SettlableInterestDays { get; set; }
        public decimal SettlableInterest { get; set; }
    }
    public class PrintInfo
    {
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public string Operator { get; set; }
        //For Withdraw
        public decimal Interest { get; set; }
        public string VoucherNumber{get;set;}
        public string MobilePhoneNumber{get;set;}

    }

    public class LenderEditDto{
        public Guid ID{get;set;}
        public string Name{get;set;}
        public string IDNumber{get;set;}
        public string MobilePhoneNumber{get;set;}
        public string Memo{get;set;}
        public DateTimeOffset CreateDate{get;set;}
        public DateTimeOffset ModifyDate{get;set;}
        public DateTimeOffset? InvalidDate{get;set;}
        public bool Invalid{get;set;}
    }
}