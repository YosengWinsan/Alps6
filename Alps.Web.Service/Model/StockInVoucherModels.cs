using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alps.Domain.StockMgr;
namespace Alps.Web.Service.Model
{
    public class StockInVoucherListDto
    {
        public Guid ID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset ConfirmTime { get; set; }
        public string Supplier { get; set; }
        public string Department { get; set; }
        public decimal TotalAuxiliaryQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Confirmer { get; set; }
        public string Status { get; set; }

    }
    public class StockInVoucherItemDto : IProductStockItem
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
    public class StockInVoucherEditDto
    {
        public Guid ID { get; set; }
        public Guid SupplierID { get; set; }
        public Guid DepartmentID { get; set; }
        public int Status { get; set; }
        public IEnumerable<StockInVoucherItemDto> Items { get; set; }
    }
    public class StockInVoucherDetailDto{
        public Guid ID{get;set;}
        public string Supplier{get;set;}
        public string Department{get;set;}
        public int StatusValue{get;set;}
        public string Status{get;set;}
        public decimal TotalQuantity{get;set;}
        public decimal TotalAuxiliaryQuantity{get;set;}
        public decimal TotalAmount{get;set;}
        public IEnumerable<StockInVoucherItemDto> Items { get; set; }
    }

}
