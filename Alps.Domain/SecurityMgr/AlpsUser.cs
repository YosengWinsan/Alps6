
using System.Collections.Generic;
using System.Linq;
namespace Alps.Domain.SecurityMgr
{
    public class AlpsUser : EntityBase
    {
        public string IDName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<AlpsRole> Roles { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string IdentityNumber { get; set; }

        public static AlpsUser Create(string idName, string password,string name, string identityNumber, string mobilePhoneNumber)
        {
            return new AlpsUser() { IDName = idName, Password = password,Name=name, IdentityNumber = identityNumber, MobilePhoneNumber = mobilePhoneNumber, Roles = new HashSet<AlpsRole>() };
        }
        public string GetRoles()
        {
            return string.Join(",",this.Roles.Select(p=>p.Name));
        }
        public void AddRole(AlpsRole role)
        {
            if (this.Roles.Count(p => p.ID == role.ID) == 0)
                this.Roles.Add(role);
        }
        public void RemoveRole(AlpsRole role)
        {
            AlpsRole r = this.Roles.FirstOrDefault(p => p.ID == role.ID);
            if (r!=null)
                this.Roles.Remove(r);
        }
    }
}
