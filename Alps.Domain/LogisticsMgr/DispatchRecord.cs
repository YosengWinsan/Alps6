using System;
using System.Collections.Generic;

namespace Alps.Domain.LogisticsMgr
{
    public class DispatchRecord:EntityBase{
        //public Guid CarID{get;set;}
        public string CarNumber{get;set;}
        public DispatchType Type{get;set;}     
        public DateTimeOffset CreateTime{get;set;}   
        public DateTimeOffset ModifyTime{get;set;}
        public string Creater{get;set;}
        public DispatchRecordStatus Status{get;set;}
        public ICollection<WeightList> WeightLists{get;set;}

        public DispatchRecord()
        {
            this.WeightLists=new HashSet<WeightList>();
            this.Status=DispatchRecordStatus.Normal;
        }

    }
    
}