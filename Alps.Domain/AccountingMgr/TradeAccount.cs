using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.AccountingMgr
{
    public class TradeAccount : EntityBase
    {
        [Display(Name = "账户名")]
        public string Name { get; set; }
        [Display(Name = "银行账号")]
        public string BankAccount { get; set; }
        [Display(Name = "手机")]
        public string CellPhoneNumber { get; set; }
        [Display(Name="库存维护")]
        public bool InventoryManagement { get; set; }
        [Display(Name = "帐户类型")]
        //public ICollection <TradeAccountType> Types { get; set; }
        public TradeAccountType Types { get; set; }
        //public System.Collections.ObjectModel.Collection<string> Catagorys { get; set; }
        public string Test { get; set; }
        public TradeAccount()
        {
            //Types = new List<TradeAccountType>();
        }
        
        public static TradeAccount Create(string name, TradeAccountType type,string cellPhoneNumber, string bankAccount)
        {
            var ta= new TradeAccount() { Name = name,  BankAccount = bankAccount ,CellPhoneNumber=cellPhoneNumber};
            //ta.Types.Add(type);
            return ta;
        }
    }
    
}
