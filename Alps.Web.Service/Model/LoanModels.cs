using System;
using System.Collections.Generic;
using Alps.Domain.LoanMgr;

namespace Alps.Web.Service.Model
{
    public class LoanVoucherListDto
    {
        public Guid ID { get; set; }
        public string Lender { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public bool InterestSettlable { get; set; }
    }
    public class DepositDto
    {
        public Guid LenderID { get; set; }
        public decimal Amount { get; set; }
        public string VoucherNumber { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Memo { get; set; }
    }
    public class WithdrawDto
    {
        public DateTimeOffset OperateTime { get; set; }
        public string Memo { get; set; }
        public Guid LoanVoucherID { get; set; }
        public decimal Amount { get; set; }
    }
    public class SettleInterestDto
    {
        public DateTimeOffset OperateTime { get; set; }
        public string Memo { get; set; }
        public Guid LoanVoucherID { get; set; }
    }
    public class WaterBillDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Interest { get; set; }
        public LoanRecordType Type { get; set; }

    }
    public class LoanRecordDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal Interest { get; set; }
        public LoanRecordType Type { get; set; }
        public Guid LoanVoucherID { get; set; }
        public bool IsInvalid { get; set; }

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
        public Guid ID { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public string Operator { get; set; }
        //For Withdraw
        public decimal Interest { get; set; }
        public string VoucherNumber { get; set; }
        public string MobilePhoneNumber { get; set; }

    }

    public class LenderEditDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string IDNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Memo { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset ModifyDate { get; set; }
        public DateTimeOffset? InvalidDate { get; set; }
        public bool Invalid { get; set; }
    }
    public class PrintInfoRequest
    {
        public LoanRecordType Type { get; set; }
        public Guid ID { get; set; }
    }
    public class LoanVoucherDetailDto
    {
        public DateTimeOffset OperateTime { get; set; }
        public string Lender { get; set; }
        public decimal Amount { get; set; }
        public decimal DepositAmount { get; set; }
        public ICollection<LoanRecordDto> Records { get; set; }
        public bool Isinvalid { get; set; }

    }
    public class InterestRateListDto
    {
        public DateTimeOffset PublishDate { get; set; }
        public string Publisher { get; set; }
        public decimal Rate { get; set; }
        public DateTimeOffset StartExecutionDate { get; set; }
    }
    public class InterestRatePublishDto
    {
        public decimal Rate { get; set; }
        public DateTimeOffset StartExecutionDate { get; set; }
    }
    public class VoucherImportDto
    {
        public string Lender { get; set; }
        public decimal Amount { get; set; }
        public string VoucherNumber { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Memo { get; set; }
        public DateTimeOffset ReWriteTime { get; set; }
    }
    public class WithdrawImportDto
    {
        public string Lender { get; set; }
        public decimal Amount { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTimeOffset DepositDate { get; set; }
        public DateTimeOffset Date { get; set; }

    }
    public class VoucherSummaryDto
    {
        public string Lender { get; set; }
        public decimal TotalAmount { get; set; }
        public int Count { get; set; }
    }
        public class SettlableInterestSummaryDto
    {
        public string Lender { get; set; }
        public decimal TotalInterest { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount{get;set;}
    }
    public class LoanSettingEditDto
    {
        public int MinDepositDay { get; set; }
        public int MinDepositAmount { get; set; }
        public ICollection<InterestRateEditDto> InterestRates { get; set; }
    }
    public class InterestRateEditDto
    {
        public Guid ID { get; set; }
        public string Publisher { get; set; }

        public decimal Rate { get; set; }
        public DateTimeOffset StartExecutionDate { get; set; }
        public DateTimeOffset PublishDate { get; set; }

    }


}