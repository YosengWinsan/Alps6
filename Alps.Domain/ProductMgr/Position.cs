using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.ProductMgr
{
    public class Position:EntityBase 
    {
        [Display(Name="仓位号")]
        public string Number { get; set; }
        [Display(Name = "仓位名称")]
        public string Name { get; set; }
        [Display(Name = "仓库")]
        public string Warehouse { get; set; }
    }
}
