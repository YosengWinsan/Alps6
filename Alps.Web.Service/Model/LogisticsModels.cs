using System;
using System.Collections.Generic;
using Alps.Domain.LogisticsMgr;

namespace Alps.Web.Service.Model
{
    public class DispatchRecordDto{
        public Guid ID{get;set;}
        public string Status{get;set;}
        public string Type{get;set;}
        public string CarNumber{get;set;}
        public IEnumerable<WeightListDto> WeightLists{get;set;}

    }
    public class WeightListDto{
        public decimal GrossWeight{get;set;}
        public decimal TareWeight{get;set;}
    }
}