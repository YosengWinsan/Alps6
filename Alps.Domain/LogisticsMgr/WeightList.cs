using System;

namespace Alps.Domain.LogisticsMgr
{
    public class WeightList:EntityBase
    {
        public Guid DispatchRecordID{get;set;}
        public virtual DispatchRecord DispatchRecord{get;set;}
        public decimal GrossWeight{get;set;}
        public decimal TareWeight{get;set;}

        public DateTimeOffset GrossWeightTime{get;set;}
        public string GrossWeightOperator{get;set;}
        public string TareWeightOperator{get;set;}
        public DateTimeOffset TareWeightTime{get;set;}
    }
    
}