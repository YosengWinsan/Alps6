
using System;

namespace Alps.Domain.LoanMgr
{
    public class Lender : EntityBase
    {
        public string Name { get; set; }
        public string IDNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Password { get; set; }
        public string Memo{get;set;}
        public bool Invalid{get;set;}
        public DateTimeOffset CreateDate{get;set;}
        public DateTimeOffset ModifyDate{get;set;}
        public DateTimeOffset? InvalidDate{get;set;}
        public static Lender Create(string name,string idNumber,string mobilePhoneNumber,string memo="")
        {
            Lender lender=new Lender(){
                Name=name,IDNumber=idNumber,MobilePhoneNumber=mobilePhoneNumber,Memo=memo,Invalid=false,
                CreateDate=DateTimeOffset.Now,ModifyDate=DateTimeOffset.Now,InvalidDate=null
            };
            return lender;
        }
        public void Invalidate(){
            this.Invalid=true;
            this.InvalidDate=DateTimeOffset.Now;
        }
    }
}