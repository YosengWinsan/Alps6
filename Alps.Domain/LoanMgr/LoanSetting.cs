using System;
using System.Collections.Generic;

namespace Alps.Domain.LoanMgr
{
    public class LoanSetting:EntityBase
    {
        public int MinDepositDay { get; set; }
        public int MinDepositAmount { get; set; }
        public ICollection<InterestRate> InterestRates { get; set; }
        public static LoanSetting Create(int minDepositDay, int MinDepositAmount)
        {
            return new LoanSetting() { MinDepositAmount = MinDepositAmount, MinDepositDay = minDepositDay, InterestRates = new HashSet<InterestRate>() };

        }
        public ICollection<InterestRate> PublishInterestRate(DateTimeOffset seDate, decimal rate, string publisher)
        {
            InterestRate ir = InterestRate.Create(seDate, rate, publisher);
            this.InterestRates.Add(ir);
            return this.InterestRates;
        }
    }


}