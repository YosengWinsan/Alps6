using System;

namespace Alps.Domain.LoanMgr

{
    public class InterestRate:EntityBase
    {
        public DateTimeOffset PublishDate { get; set; }
        public string Publisher { get; set; }
        public decimal Rate { get; set; }
        public DateTimeOffset StartExecutionDate { get; set; }
            public static InterestRate Create(DateTimeOffset seDate, decimal rate, string publisher)
    {
        return new InterestRate() { PublishDate = DateTimeOffset.Now, Publisher = publisher, Rate = rate, StartExecutionDate = seDate };
    }
    }

}