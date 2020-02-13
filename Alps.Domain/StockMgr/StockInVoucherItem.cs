using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.StockMgr
{
    public class StockInVoucherItem:EntityBase
    {
        public Guid StockInVoucherID { get; set; }
        public Guid ProductSkuID { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal AuxiliaryQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal Quantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }
        public Guid PositionID { get; set; }
        public string SerialNumber { get; set; }

        public virtual StockInVoucher StockInVoucher { get; set; }
    }
}
