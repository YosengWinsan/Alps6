using System;
using Alps.Domain.Common;
using Alps.Domain.StockMgr;

namespace Alps.Domain.AccountingMgr
{
    public class AccountPayable : EntityBase
    {
        public decimal Amount { get; set; }
        public Guid StockInVoucherID{get;set;}
        public StockInVoucher StockInVoucher{get;set;}
        public Guid SupplierID{get;set;}
        //public Guid
    }
}