using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.LoanMgr
{
    public class LoanVoucher : EntityBase
    {

        public DateTimeOffset DepositDate { get; set; }
        public Guid LenderID { get; set; }
        public virtual Lender Lender { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginAmount { get; set; }
        public DateTimeOffset InterestSettlementDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        public string VoucherNumber { get; set; }
        public string HashCode { get; set; }
        public virtual ICollection<WithdrawRecord> WithdrawRecords { get; set; }
        public String Operator { get; set; }
        public DateTimeOffset ModifyDate { get; set; }
        public bool IsInvalid { get; set; }
        public DateTimeOffset? InvalidDate { get; set; }
        protected LoanVoucher()
        {
            this.ModifyDate = DateTimeOffset.Now;
            this.WithdrawRecords = new List<WithdrawRecord>();
        }
        public static LoanVoucher Create(Guid lenderID, decimal amount, decimal interestRate, string voucherNumber)
        {
            return Create(lenderID, amount, interestRate, voucherNumber, DateTime.Now);
        }
        public static LoanVoucher Create(Guid lenderID, decimal amount, decimal interestRate, string voucherNumber, DateTimeOffset date)
        {
            LoanVoucher v = new LoanVoucher();
            v.LenderID = lenderID;
            v.Amount = amount;
            v.InterestRate = interestRate;
            v.VoucherNumber = voucherNumber;
            v.DepositDate = date;
            v.InterestSettlementDate = v.DepositDate;
            v.ModifyDate = DateTimeOffset.Now;
            return v;
        }
        public static DateTimeOffset GetSettlableDate()
        {
            DateTimeOffset settlableDate = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));

            //var settlableDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            settlableDate = settlableDate.AddMonths(DateTimeOffset.Now.Month / 3 * 3 - DateTimeOffset.Now.Month);
            return settlableDate;
        }
        public static int GetInterestDay(DateTimeOffset startdate, DateTimeOffset stopdate)
        {

            DateTime fromdate = startdate.LocalDateTime;
            DateTime todate = stopdate.LocalDateTime;

            if (fromdate.Date >= todate.Date)
                return 0;

            var enddate = todate.Date.AddDays(-1);
            var months = (enddate.Year - fromdate.Year) * 12 + (enddate.Month - fromdate.Month) - 1;

            var fromMonthDays = fromdate.Date.AddDays(1 - fromdate.Day).AddMonths(1).AddDays(-1).Day;
            if (months < 0)
            {
                var days = enddate.Subtract(fromdate.Date).Days + 1;
                return days == fromMonthDays ? 30 : 0;
            }
            var fromDays = fromMonthDays + 1 - fromdate.Date.Day;
            if (fromDays > 30 || fromDays == fromMonthDays)
                fromDays = 30;

            var endMonthDays = enddate.AddDays(1 - enddate.Day).AddMonths(1).AddDays(-1).Day;
            var endDays = enddate.Day;
            if (endDays > 30 || endDays == endMonthDays)
                endDays = 30;

            var interestDays = months * 30 + fromDays + endDays;
            interestDays = interestDays < 30 ? 0 : interestDays;

            return interestDays;
        }
        public WithdrawRecord Withdraw(decimal amount)
        {
            if (amount > this.Amount)
                throw new DomainException("取款金额超过存款金额");
            WithdrawRecord r = new WithdrawRecord();
            r.Date = DateTimeOffset.Now;
            r.DepositDate = this.InterestSettlementDate;
            r.Amount = amount;
            r.InterestRate = this.InterestRate;
            r.Interest = GetInterestDay(this.InterestSettlementDate, r.Date) * r.Amount * r.InterestRate / 30;
            r.LoanVoucherID = this.ID;
            r.Remark = GetInterestDay(this.InterestSettlementDate, r.Date).ToString();
            this.WithdrawRecords.Add(r);
            this.Amount = this.Amount - amount;
            this.ModifyDate = DateTimeOffset.Now;
            return r;
        }
        public WithdrawRecord InterestSettle()
        {
            var settlableDate = GetSettlableDate();
            if (settlableDate.Subtract(this.InterestSettlementDate).Days < 30)
                throw new DomainException("天数不够");
            WithdrawRecord r = new WithdrawRecord();

            r.Date = DateTimeOffset.Now;
            r.DepositDate = this.InterestSettlementDate;
            r.Amount = 0;
            r.InterestRate = this.InterestRate;
            r.Interest = GetInterestDay(this.InterestSettlementDate, settlableDate) * this.Amount * r.InterestRate / 30;
            r.LoanVoucherID = this.ID;
            r.Remark = GetInterestDay(this.InterestSettlementDate, settlableDate).ToString();
            this.WithdrawRecords.Add(r);
            this.InterestSettlementDate = settlableDate;
            this.ModifyDate = DateTimeOffset.Now;
            return r;
        }
        public void Invalid()
        {
            if (!this.IsInvalid)
            {
                foreach(var r in this.WithdrawRecords){
                    
                }
                this.IsInvalid = true;
                this.InvalidDate = DateTimeOffset.Now.Date;
            }
            else
                throw new DomainException("不可重复作废");

        }
    }
}