using System;
using Alps.Domain.Common;

using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.SaleMgr
{
    public class Commodity : EntityBase
    {
        [Display(Name="商品名")]
        public string Name { get; set; }
        [Display(Name="包装数量")]
        public decimal PackingQuantity { get; set; }
        [Display(Name="商品描述")]
        public string Description { get; set; }
        [Display(Name="库存数量")]
        public decimal StockQuantity { get; set; }
        public Unit Unit { get; set; }
        [Display(Name="定价")]
        public decimal ListPrice { get; set; }
        [Display(Name="物料名")]
        public Guid MaterialID { get; set; }
        [Display(Name="期货否")]
        public bool IsFutures { get; set; }
        [Display(Name="交货日期")]
        public DateTime? DateOfDelivery { get; set; }

        public static Commodity Create(Guid materialID, string name, string description, decimal listPrice, decimal stockQuantity)
        {
            return new Commodity() { MaterialID = materialID, Name = name, Description = description, ListPrice = listPrice, StockQuantity = stockQuantity };
        }

    }
}
