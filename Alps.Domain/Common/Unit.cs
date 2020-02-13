using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.Common
{
    public class Unit : EntityBase
    {
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "级别")]
        public int Group { get; set; }
        [Display(Name = "基本单位")]
        public bool IsBaseUnit { get; set; }
        [Display(Name = "换算率")]
        [Column(TypeName="decimal(18,2)")]
        public decimal RateOfExchange { get; set; }

        public static Unit Create(string name, int group,bool isBaseUnit,decimal rateOfExchange=1)
        {
            if (isBaseUnit)
                rateOfExchange = 1;
            return new Unit { Name = name, Group = group,IsBaseUnit=isBaseUnit,RateOfExchange=rateOfExchange };
        }
    }

}
