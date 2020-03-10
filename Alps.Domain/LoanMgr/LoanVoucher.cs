using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
//using System.Linq;

namespace Alps.Domain.LoanMgr
{

    public class LoanVoucher : EntityBase
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
        public DateTimeOffset VoucherTime { get; set; }
        public DateTimeOffset DepositTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DepositAmount { get; set; }
        protected LoanVoucher()
        {
            this.CreateTime = DateTimeOffset.Now;
            this.Records = new HashSet<LoanRecord>();
        }
        public static LoanVoucher Create(Guid lenderID, string creater)
        {
            LoanVoucher v = new LoanVoucher();
            v.Creater = creater;
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
            this.Amount = this.Amount + record.Amount;
            this.DepositAmount = record.Amount;
            this.VoucherTime = DateTimeOffset.Now;

            this.InterestSettlementDate = (operateTime >= DateTimeOffset.Parse("2019/12/1 0:00:00 +08:00") ? operateTime : DateTimeOffset.Parse("2019/12/1 0:00:00 +08:00"));

            return record;
        }
        public LoanRecord Withdraw(DateTimeOffset operateTime, decimal amount, string memo, string creater, IList<InterestRate> rates)
        {
            if (this.IsInvalid)
                throw new DomainException("已作废的条子无法取款");
            // if (!this.CanSettleInterest())
            //     throw new DomainException("无法结息");
            var interest = CalculateInterest(rates, operateTime, amount, 0); ;

            LoanRecord record = LoanRecord.Create(LoanRecordType.Withdraw, operateTime, amount, interest, memo, creater);
            this.Records.Add(record);
            this.Amount = this.Amount - record.Amount;
            this.VoucherTime = DateTimeOffset.Now;
            return record;
        }
        public bool CanSettleInterest()
        {
            return this.InterestSettlementDate < GetSettlableDate();
        }
        public LoanRecord SettleInterest(DateTimeOffset operateTime, string memo, string creater, IList<InterestRate> rates)
        {
            if (this.IsInvalid)
                throw new DomainException("已作废的条子无法结息");
            if (!this.CanSettleInterest())
                throw new DomainException("无法结息");
            var interest = CalculateQuarterInterest(rates);
            LoanRecord record = LoanRecord.Create(LoanRecordType.SettleInterest, operateTime, this.Amount, interest, memo, creater);
            this.Records.Add(record);
            record.Memo = this.InterestSettlementDate.ToString();
            this.InterestSettlementDate = GetSettlableDate();
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
                case LoanRecordType.SettleInterest:
                    this.InterestSettlementDate = DateTimeOffset.Parse(r.Memo);
                    break;
            }
        }
        public void ReWriteVoucher(DateTimeOffset printTime)
        {
            this.VoucherTime = printTime;
        }
        public decimal CalculateInterest(IList<InterestRate> rates, DateTimeOffset settlingDate, decimal amount, int notEnoughAMonthSubDay = 2)
        {
            var firstRate = rates.Where(p => p.StartExecutionDate <= this.InterestSettlementDate).OrderByDescending(p => p.StartExecutionDate).FirstOrDefault();
            if (firstRate == null)
                throw new DomainException("未设置利率");
            var calcRates = rates.Where(p => p.StartExecutionDate >= firstRate.StartExecutionDate).OrderBy(p => p.StartExecutionDate).ToList();
            DateTimeOffset startDate, endDate;
            decimal totalInterest = 0;
            decimal rate;
            for (var i = 0; i < calcRates.Count; i++)
            {
                startDate = this.InterestSettlementDate >= calcRates[i].StartExecutionDate ? this.InterestSettlementDate : calcRates[i].StartExecutionDate;
                if (i == calcRates.Count - 1)
                    endDate = settlingDate;
                else
                    endDate = calcRates[i + 1].StartExecutionDate;

                rate = calcRates[i].Rate;
                totalInterest = totalInterest + CalculatePeriodInterest(rate, startDate, endDate, amount, notEnoughAMonthSubDay);
            }
            return Math.Round(totalInterest);
        }
        public decimal CalculateVoucherInterest(IList<InterestRate> rates)
        {
            return CalculateInterest(rates, DateTimeOffset.Now, this.Amount);
        }
        public decimal CalculateQuarterInterest(IList<InterestRate> rates)
        {
            return CalculateInterest(rates, GetSettlableDate(), this.Amount);
        }
        private decimal CalculatePeriodInterest(decimal rate, DateTimeOffset startDate, DateTimeOffset endDate, decimal amount, int notEnoughAMonthSubDay)
        {
            DateTime fromdate = startDate.LocalDateTime;
            DateTime todate = endDate.LocalDateTime;

            if (fromdate.Date >= todate.Date)
                return 0;

            var enddate = todate.Date.AddDays(-1);
            var months = (enddate.Year - fromdate.Year) * 12 + (enddate.Month - fromdate.Month) - 1;

            var fromMonthDays = fromdate.Date.AddDays(1 - fromdate.Day).AddMonths(1).AddDays(-1).Day;
            var interestDays = 0;
            if (months < 0)
            {
                var days = enddate.Subtract(fromdate.Date).Days + 1;

                if (enddate.Month > fromdate.Month)
                    days = days - (fromdate.Date.AddDays(1 - fromdate.Day).AddMonths(1).AddDays(-1).Day - 30);
                else
                {
                    if (enddate.Day == enddate.AddDays(1 - enddate.Day).AddMonths(1).AddDays(-1).Day && enddate.Month == 2)
                    {
                        days = days - (fromdate.Date.AddDays(1 - fromdate.Day).AddMonths(1).AddDays(-1).Day - 30);
                    }
                }
                interestDays = days - notEnoughAMonthSubDay;
            }
            else
            {
                var fromDays = fromMonthDays + 1 - fromdate.Date.Day - (fromMonthDays > 30 && fromdate.Day <= 30 ? 1 : 0);
                if (fromDays > 30 || fromDays == fromMonthDays)
                    fromDays = 30;

                var endMonthDays = enddate.AddDays(1 - enddate.Day).AddMonths(1).AddDays(-1).Day;
                var endDays = enddate.Day;
                if (endDays > 30 || endDays == endMonthDays)
                    endDays = 30;
                interestDays = months * 30 + fromDays + endDays;
            }
            return interestDays * amount * rate / 30;
        }
    }
}