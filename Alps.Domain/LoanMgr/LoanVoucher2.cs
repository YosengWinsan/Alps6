using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;

namespace Alps.Domain.LoanMgr
{

    public class LoanVoucher2
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
        public int IdentifyingCode { get; set; }

        public virtual Lender Lender { get; set; }
        public virtual ICollection<LoanRecord> Records { get; set; }

        public bool IsInvalid { get; set; }
        public DateTimeOffset? InvalidDate { get; set; }
        public string InvalidMaker{get;set;}
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
        public void Deposit(DateTimeOffset operateTime, decimal amount, string memo, string creater)
        {
            foreach (var r in this.Records)
            {
                if (r.Type == LoanRecordType.Deposit)
                    throw new DomainException("已经存在存款记录");
            }
            LoanRecord record = LoanRecord.Create(LoanRecordType.Deposit, operateTime, amount, memo, creater);
            this.Records.Add(record);
        }
        public void Withdraw(DateTimeOffset operateTime, decimal amount, string memo, string creater)
        {
            LoanRecord record = LoanRecord.Create(LoanRecordType.Withdraw, operateTime, amount, memo, creater);
            this.Records.Add(record);
        }
        public void SettleInterest(DateTimeOffset operateTime, decimal amount, string memo, string creater)
        {
            LoanRecord record = LoanRecord.Create(LoanRecordType.SettleInterest, operateTime, amount, memo, creater);
            this.Records.Add(record);
        }
        public void Invalid(string invalidMaker)
        {
            foreach (var r in this.Records)
            {
                if (!r.IsInvalid)
                    throw new DomainException("存在存取记录，不能作废");
            }
            if(!IsInvalid)
             throw new DomainException("已作废，不能重复作废");
             this.IsInvalid=true;
             this.InvalidDate=DateTimeOffset.Now;
             this.InvalidMaker=invalidMaker;
        }
    }

}