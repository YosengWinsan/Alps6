using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.StockMgr
{
    public class StockOutVoucherItem:EntityBase
    {
        public Guid StockOutVoucherID { get; set; }
        public Guid ProductSkuID { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal AuxiliaryQuantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal Quantity { get; set; }
         [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }
        public Guid PositionID { get; set; }
        public string SerialNumber { get; set; }

        public virtual StockOutVoucher StockOutVoucher { get; set; }
    }
}
