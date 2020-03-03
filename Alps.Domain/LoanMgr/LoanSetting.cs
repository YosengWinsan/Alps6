using System.Collections.Generic;

namespace Alps.Domain.LoanMgr
{
    public class LoanSetting
    {
        public int MinDepositDay { get; set; }
        public int MinDepositAmount { get; set; }
        public ICollection<InterestRate> InterestRates { get; set; }
    }

}