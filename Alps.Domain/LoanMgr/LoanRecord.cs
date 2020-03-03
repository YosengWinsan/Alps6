using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.LoanMgr
{
    public class LoanRecord : EntityBase
    {
        public DateTimeOffset CreateTime { get; set; }
        public string Creater { get; set; }

        public DateTimeOffset OperateTime { get; set; }
        public Guid LoanVoucherID { get; set; }
        public virtual LoanVoucher2 LoanVoucher { get; set; }
        public LoanRecordType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Interest { get; set; }
        public string Memo { get; set; }

        public string Reviewer { get; set; }
        public DateTimeOffset ReviewTime { get; set; }

        public bool IsInvalid { get; set; }
        public DateTimeOffset? InvalidDate { get; set; }
        public string InvalidMaker { get; set; }

        public static LoanRecord Create(LoanRecordType type, DateTimeOffset operateTime, decimal amount,decimal Interest, string memo, string creater)
        {
            return new LoanRecord()
            {
                Type = type,
                OperateTime = operateTime,
                Amount = amount,
                CreateTime = DateTimeOffset.Now,
                Creater = creater,
                Memo = memo,
                Interest=Interest
            };
        }
    }


}