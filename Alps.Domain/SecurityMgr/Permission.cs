using System;

namespace Alps.Domain.SecurityMgr
{
    
    public class Permission:EntityBase{
        public Guid RoleID{get;set;}
        public Guid ResourceID{get;set;}
        public virtual AlpsRole Role{get;set;}
        public virtual AlpsResource Resource{get;set;}

        public static Permission Create(Guid resourceID,Guid roleID){
            return new Permission{ResourceID=resourceID,RoleID=roleID};
        }
    }
}