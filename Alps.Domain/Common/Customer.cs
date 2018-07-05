
using Alps.Domain.AccountingMgr;

namespace Alps.Domain.Common
{
    public class Customer : EntityBase
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Contact{get;set;}
        public static Customer Create(string name, Address address)
        {
            //Customer c=TradeAccount.Create("",TradeAccountType.Customer,"","");

            var c = new Customer() { Name = name, Address = address };
            return c;
        }
    }

}
