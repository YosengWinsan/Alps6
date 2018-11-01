using System;

namespace Alps.Domain.LogisticsMgr
{
    public class DispatchRecord:EntityBase{
        public Guid CarID{get;set;}
        public DispatchType Type{get;set;}
        
        public WeightList WeightList{get;set;}

    }
    
}