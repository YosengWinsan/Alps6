using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
//using System.Linq;

namespace Alps.Domain.LoanMgr
{

    public class LoanVoucher2 : EntityBase
    {
        public DateTimeOffset CreateTime { get; set; }
        public string Creater { get; set; }
        public Guid LenderID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTimeOffset InterestSettlementDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }
        public string VoucherNumber { get; set; }
        public string IdentityCode { get; set; }

        public virtual Lender Lender { get; set; }
        public virtual ICollection<LoanRecord> Records { get; set; }

        public bool IsInvalid { get; set; }
        public DateTimeOffset? InvalidDate { get; set; }
        public string InvalidMaker { get; set; }

        public DateTimeOffset DepositTime { get; set; }
        protected LoanVoucher2()
        {
            this.CreateTime = DateTimeOffset.Now;
            this.Records = new HashSet<LoanRecord>();
        }
        public static LoanVoucher2 Create(Guid lenderID, string creater)
        {
            LoanVoucher2 v = new LoanVoucher2();
            v.LenderID = lenderID;
            v.Amount = 0;

            // v.InterestRate = interestRate;
            // v.VoucherNumber = voucherNumber;
            // v.DepositDate = date;
            // v.InterestSettlementDate = v.DepositDate;
            // v.ModifyDate = DateTimeOffset.Now;
            return v;
        }
        public LoanRecord Deposit(DateTimeOffset operateTime, decimal amount, string memo, string creater)
        {
            foreach (var r in this.Records)
            {
                if (r.Type == LoanRecordType.Deposit)
                    throw new DomainException("已经存在存款记录");
            }
            LoanRecord record = LoanRecord.Create(LoanRecordType.Deposit, operateTime, amount, 0, memo, creater);
            this.Records.Add(record);
            this.DepositTime = operateTime;
            return record;
        }
        public LoanRecord Withdraw(DateTimeOffset operateTime, decimal amount, string memo, string creater)
        {
            var interest = 0;
            LoanRecord record = LoanRecord.Create(LoanRecordType.Withdraw, operateTime, amount, interest, memo, creater);
            this.Records.Add(record);
            return record;
        }
        public LoanRecord SettleInterest(DateTimeOffset operateTime, string memo, string creater)
        {
            var interest = 0;
            LoanRecord record = LoanRecord.Create(LoanRecordType.SettleInterest, operateTime, 0, interest, memo, creater);
            this.Records.Add(record);
            return record;
        }
        public void Invalid(string invalidMaker)
        {
            foreach (var r in this.Records)
            {
                if (!r.IsInvalid)
                    throw new DomainException("存在存取记录，不能作废");
            }
            if (!IsInvalid)
                throw new DomainException("已作废，不能重复作废");
            this.IsInvalid = true;
            this.InvalidDate = DateTimeOffset.Now;
            this.InvalidMaker = invalidMaker;
        }

        public static DateTimeOffset GetSettlableDate()
        {
            DateTimeOffset settlableDate = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
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
        public void InvalidRecord(Guid id, string invalidMaker)
        {
            var r = this.Records.OrderByDescending(p => p.CreateTime).FirstOrDefault();
            if (r == null)
                throw new DomainException("不存在存取记录");
            if (r.ID != id)
                throw new DomainException("不存在此ID");

            r.IsInvalid = true;
            r.InvalidDate = DateTimeOffset.Now;
            r.InvalidMaker = invalidMaker;
            switch (r.Type)
            {
                case LoanRecordType.Withdraw:
                    this.Amount = this.Amount + r.Amount;
                    break;
                case LoanRecordType.Deposit:
                    this.Amount = this.Amount - r.Amount;
                    this.IsInvalid = true;
                    this.InvalidDate = DateTimeOffset.Now;
                    this.InvalidMaker = invalidMaker;
                    break;
            }
        }
    }
}