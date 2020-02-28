
namespace Alps.Web.Service.Model
{
    public class LoginDto
    {
        public string Username{get;set;}
        public string Password{get;set;}
    }
    public class RegisterDto{
        public string Username{get;set;}
        public string RealName{get;set;}
        public string Password{get;set;}
        public string IdentityNumber{get;set;}
        public string MobilePhoneNumber{get;set;}
    }
    
}