using System;

namespace Alps.Domain.LoanMgr

{
    public class InterestRate
    {
        public DateTimeOffset PublishDate { get; set; }
        public string Publisher{get;set;}
        public decimal Rate { get; set; }
        public DateTimeOffset StartExecutionDate { get; set; }
    }
}