

using System;
using System.Collections.Generic;

namespace Alps.Domain.SecurityMgr
{
    public class AlpsResource : EntityBase
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public virtual ICollection<Permission> Permissions{get;set;}
    }
}