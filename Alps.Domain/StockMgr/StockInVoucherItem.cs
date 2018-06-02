using System;

namespace Alps.Domain.StockMgr
{
    public class StockInVoucherItem:EntityBase
    {
        public Guid StockInVoucherID { get; set; }
        public Guid ProductSkuID { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid PositionID { get; set; }
        public string SerialNumber { get; set; }

        public virtual StockInVoucher StockInVoucher { get; set; }
    }
}
