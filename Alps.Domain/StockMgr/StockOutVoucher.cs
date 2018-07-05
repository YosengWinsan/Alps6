using Alps.Domain.AccountingMgr;
using Alps.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Domain.StockMgr
{
    public class StockOutVoucher:EntityBase
    {
        public Guid CustomerID { get; set; }
        public Guid DepartmentID { get; set; }
        public decimal TotalAuxiliaryQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Confirmer { get; set; }
        public DateTimeOffset? ConfirmTime { get; set; }
        public StockOutVoucherStatus Status { get; set; }
        public string Creater { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public virtual ICollection<StockOutVoucherItem> Items { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Department Department { get; set; }

        public StockOutVoucher()
        {
            Items = new HashSet<StockOutVoucherItem>();
        }
    
    public static StockOutVoucher Create(Guid departmentID, Guid customerID,  string creater)
    {
        StockOutVoucher voucher = new StockOutVoucher()
        {
            CustomerID = customerID,
            DepartmentID = departmentID,
            Creater = creater,
            CreateTime = DateTimeOffset.Now
        };
        return voucher;
    }
    public StockOutVoucherItem AddItem(Guid positionID, Guid productSkuID, string serialNumber, decimal quantity, decimal auxiliaryQuantity, decimal price, bool reFreshQuantity = true)
    {
        var item = new StockOutVoucherItem()
        {
            ProductSkuID = productSkuID,
            Quantity = quantity,
            Price = price,
            PositionID = positionID,
            AuxiliaryQuantity = auxiliaryQuantity,
            SerialNumber = serialNumber ?? "",
            StockOutVoucherID = this.ID
        };
        this.Items.Add(item);
        if (reFreshQuantity)
            ReFreshQuantity();
        return item;
    }
    public void RemoveItem(Guid id, bool reFreshQuantity = false)
    {
        var item = this.Items.FirstOrDefault(p => p.ID == id);
        if (item == null)
            throw new DomainException("无此ID");
        this.Items.Remove(item);
        if (reFreshQuantity)
            ReFreshQuantity();
    }
    public void UpdateItem(Guid id, Guid positionID, Guid productSkuID, string serialNumber, decimal quantity, decimal auxiliaryQuantity , decimal price, bool reFreshQuantity = true)
    {
        var item = this.Items.FirstOrDefault(p => p.ID == id);
        if (item == null)
            throw new DomainException("无此ID");
        item.ProductSkuID = productSkuID;
        item.Quantity = quantity;
        item.Price = price;
        item.PositionID = positionID;
        item.SerialNumber = serialNumber;
        item.AuxiliaryQuantity = auxiliaryQuantity;

        if (reFreshQuantity)
            ReFreshQuantity();
    }
    private void ReFreshQuantity()
    {
        this.TotalQuantity = this.Items.Sum(p => p.Quantity);
        this.TotalAuxiliaryQuantity = this.Items.Sum(p => p.AuxiliaryQuantity);
        this.TotalAmount = this.Items.Sum(p => p.Price * p.Quantity);
    }
    public void UpdateItems(IEnumerable<IProductStockItem> items)
    {
        var existingItems = this.Items.ToList();
        var updatedItems = items.Where(p => this.Items.Any(k => k.ID == p.ID)).ToList();
        var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
        var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
        deletedItems.ForEach(p => this.RemoveItem(p.ID, false));
        addedItems.ForEach(p => this.AddItem(p.PositionID, p.ProductSkuID, p.SerialNumber, p.Quantity, p.AuxiliaryQuantity, p.Price, false));
        updatedItems.ForEach(p => this.UpdateItem(p.ID,p.PositionID, p.ProductSkuID,  p.SerialNumber, p.Quantity, p.AuxiliaryQuantity, p.Price, false));
        ReFreshQuantity();
    }
    public StockOutVoucherItem AddItem(IProductStockItem item)
    {
        return this.AddItem(item.PositionID, item.ProductSkuID, item.SerialNumber, item.Quantity, item.AuxiliaryQuantity, item.Price);
    }
    }
}
