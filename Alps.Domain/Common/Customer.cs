
using Alps.Domain.AccountingMgr;

namespace Alps.Domain.Common
{
    public class Customer : TradeAccount
    {
       // public string Name { get; set; }
        public static Customer Create(string name)
        {
            //Customer c=TradeAccount.Create("",TradeAccountType.Customer,"","");
            var c = new Customer() { Name = name };
            return c;
        }
    }

}
