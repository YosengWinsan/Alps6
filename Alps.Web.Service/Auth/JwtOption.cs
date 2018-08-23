namespace Alps.Web.Service.Auth
{
    public class AlpsJwtOption
    {
        public string SecurityKey {get;set;}
        public string Issuer{get;set;}
        public string Audience{get;set;}
    }
}