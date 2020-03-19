using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Domain.SecurityMgr
{
    public class AlpsRole : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AlpsRoleUser> RoleUsers { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public static AlpsRole Create(string name, string description)
        {
            return new AlpsRole() { Name = name, Description = description };
        }
        public void AddPermission(AlpsResource resource)
        {
            AddPermission(resource.ID);
        }
        public void AddPermission(Guid resourceID)
        {
            if (this.Permissions.Count(p => p.ID == resourceID) == 0)
                this.Permissions.Add(Permission.Create(resourceID, this.ID));
        }
        public void RemovePermission(AlpsResource resource)
        {
            RemovePermission(resource.ID);
        }
        public void RemovePermission(Guid resourceID)
        {
            Permission r = this.Permissions.FirstOrDefault(p => p.ResourceID == resourceID);
            if (r != null)
                this.Permissions.Remove(r);
        }
    }
}
