using Alps.Domain.StockMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alps.Web.Service.Model
{
    public class StockOutVoucherListDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset ConfirmTime { get; set; }
        public string Customer { get; set; }
        public string Department { get; set; }
        public decimal TotalAuxiliaryQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Confirmer { get; set; }
        public string Status { get; set; }

    }
    public class StockOutVoucherItemDto : IProductStockItem
    {
        public Guid ID { get; set; }
        public Guid ProductSkuID { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid PositionID { get; set; }
        public string SerialNumber { get; set; }
        public string ProductSku { get; set; }
        public string Position { get; set; }

    }
    public class StockOutVoucherEditDto
    {
        public Guid ID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid DepartmentID { get; set; }
        public int Status { get; set; }
        public IEnumerable<StockOutVoucherItemDto> Items { get; set; }
    }
    public class StockOutVoucherDetailDto
    {
        public Guid ID { get; set; }
        public string Customer { get; set; }
        public string Department { get; set; }
        public int StatusValue { get; set; }
        public string Status { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAuxiliaryQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<StockOutVoucherDetailItemDto> Items { get; set; }
    }
    public class StockOutVoucherDetailItemDto
    {
        public Guid ID { get; set; }
        public string ProductSku { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Position { get; set; }
        public string SerialNumber { get; set; }

    }
}
