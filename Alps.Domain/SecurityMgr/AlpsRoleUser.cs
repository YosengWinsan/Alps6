using System;

namespace Alps.Domain.SecurityMgr
{
    public class AlpsRoleUser:EntityBase
    {
        public Guid RoleID { get; set; }
        public Guid UserID { get; set; }
        public AlpsRole Role { get; set; }
        public AlpsUser User { get; set; }
        public static AlpsRoleUser Create(Guid roleID,Guid userID)
        {
            return new AlpsRoleUser(){RoleID=roleID,UserID=userID};
        }
    }
}