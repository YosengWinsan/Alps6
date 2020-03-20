using System;
using System.Collections.Generic;
using Alps.Domain.AccountingMgr;

namespace Alps.Web.Service.Model
{
    public class UserListDto
    {
        public Guid ID{get;set;}
        public string IDName{get;set;} 
        public string Name{get;set;}
        public string MobilePhoneNumber{get;set;}
        public string IdentityNumber{get;set;}
        public string Roles{get;set;}
    }
    public class RoleDto
    {
        public Guid ID{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public byte[] Timestamp{get;set;}
    }
    public class UserDetailDto
    {
        public Guid ID{get;set;}
        public string IDName{get;set;} 
        public string Name{get;set;}
        public string MobilePhoneNumber{get;set;}
        public string IdentityNumber{get;set;}
        public string Roles{get;set;}
    }
    public class UserEditDto
    {
        public Guid ID{get;set;}
        public string IDName{get;set;} 
        public string Name{get;set;}
        public string MobilePhoneNumber{get;set;}
        public string IdentityNumber{get;set;}
        public string Roles{get;set;}
        public string Password{get;set;}
    }
}