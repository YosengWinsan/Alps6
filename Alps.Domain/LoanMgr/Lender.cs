
namespace Alps.Domain.LoanMgr
{
    public class Lender : EntityBase
    {
        public string Name { get; set; }
        public string IDNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Password { get; set; }
        public string Memo{get;set;}
        public static Lender Create(string name,string idNumber,string mobilePhoneNumber,string memo="")
        {
            Lender lender=new Lender(){
                Name=name,IDNumber=idNumber,MobilePhoneNumber=mobilePhoneNumber,Memo=memo
            };
            return lender;
        }
    }
}