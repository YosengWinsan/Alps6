using System;
using System.Collections.Generic;
using Alps.Domain.LogisticsMgr;

namespace Alps.Web.Service.Model
{
    public class DispatchRecordDto
    {
        public Guid ID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string CarNumber { get; set; }

        public decimal GrossWeight { get; set; }
        public decimal TareWeight { get; set; }

        public DateTimeOffset GrossWeightTime { get; set; }
        public string GrossWeightOperator { get; set; }
        public string TareWeightOperator { get; set; }
        public DateTimeOffset TareWeightTime { get; set; }
        public DateTimeOffset WeightConfirmedTime { get; set; }
        public string WeightConfirmedOperator { get; set; }
        public IEnumerable<VoucherDto> Vouchers{get;set;}
        //public IEnumerable<WeightListDto> WeightLists{get;set;}

    }
    public class VoucherDto{
        public Guid ID{get;set;}
        public string Source{get;set;}
        public string Destination{get;set;}
        public decimal AQuantity{get;set;}
        public decimal Quantity{get;set;}
        public decimal Amount{get;set;}
        public string Operator{get;set;}

    }
    public class WeightListDto
    {
        public decimal GrossWeight { get; set; }
        public decimal TareWeight { get; set; }
    }
}