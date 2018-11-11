using System;
using System.Collections.Generic;

namespace Alps.Domain.LogisticsMgr
{
    public class DispatchRecord : EntityBase
    {
        //public Guid CarID{get;set;}
        public string CarNumber { get; set; }
        public DispatchType Type { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset ModifyTime { get; set; }
        public string Creater { get; set; }
        public DispatchRecordStatus Status { get; set; }
        //public ICollection<WeightList> WeightLists{get;set;}
        public decimal GrossWeight { get; set; }
        public decimal TareWeight { get; set; }

        public DateTimeOffset GrossWeightTime { get; set; }
        public string GrossWeightOperator { get; set; }
        public string TareWeightOperator { get; set; }
        public DateTimeOffset TareWeightTime { get; set; }
        public DateTimeOffset WeightConfirmedTime{get;set;}
        public string WeightConfirmedOperator{get;set;}

        public DispatchRecord()
        {
            //this.WeightLists=new HashSet<WeightList>();
            this.Status = DispatchRecordStatus.Normal;
        }
        public static DispatchRecord Create(string carNumber, string creater)
        {
            var newDR = new DispatchRecord
            {
                CarNumber = carNumber,//WeightLists=new HashSet<WeightList>(),
                Status = DispatchRecordStatus.Normal
            ,
                CreateTime = DateTimeOffset.Now,
                Creater = creater
            };
            return newDR;
        }

    }

}