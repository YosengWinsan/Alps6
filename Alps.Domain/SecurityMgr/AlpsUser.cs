using System.Collections.Generic;
using System.Linq;
namespace Alps.Domain.SecurityMgr
{
    public class AlpsUser : EntityBase
    {
        public string IDName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<AlpsRoleUser> RoleUsers { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string IdentityNumber { get; set; }

        public static AlpsUser Create(string idName, string password,string name, string identityNumber, string mobilePhoneNumber)
        {
            return new AlpsUser() { IDName = idName, Password = password,Name=name, IdentityNumber = identityNumber, MobilePhoneNumber = mobilePhoneNumber, RoleUsers = new HashSet<AlpsRoleUser>() };
        }
        public string GetRoles()
        {
            return string.Join(",",this.RoleUsers.Select(p=>p.Role.Name));
        }
        public void AddRole(AlpsRole role)
        {
            if (this.RoleUsers.Count(p => p.RoleID == role.ID) == 0)
                this.RoleUsers.Add(AlpsRoleUser.Create(role.ID,this.ID));
        }
        public void RemoveRole(AlpsRole role)
        {
            AlpsRoleUser r = this.RoleUsers.FirstOrDefault(p => p.RoleID == role.ID);
            if (r!=null)
                this.RoleUsers.Remove(r);
        }
    }
}
