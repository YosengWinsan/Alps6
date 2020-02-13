using System;
using System.Linq;
using Alps.Domain.AccountingMgr;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Alps.Domain.ProductMgr;
using Alps.Domain.Common;
namespace Alps.Domain.PurchaseMgr
{
    public class PurchaseOrder : EntityBase
    {
        protected PurchaseOrder()
        {
            Items = new Collection<PurchaseOrderItem>();
        }
        [Display(Name = "订单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "制单人")]
        public string Creater { get; set; }
        [Display(Name = "供应商")]
        public Guid SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderItem> Items { get; set; }
        [Display(Name = "订单状态")]
        public PurchaseOrderState State { get; set; }
        [Display(Name = "总金额")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "总件数")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "总重量")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWeight { get; set; }
        public void Confirm()
        {
            if (State == PurchaseOrderState.Unconfirmed)
                State = PurchaseOrderState.Confirming;
        }
        public static PurchaseOrder Create(Guid supplierID, string creater)
        {
            return new PurchaseOrder() { SupplierID = supplierID, Creater = creater, OrderTime = DateTime.Now };
        }
        public void AddItem(ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price)
        {
            PurchaseOrderItem newPurchaseOrderItem = new PurchaseOrderItem();
            //newPurchaseOrderItem.ProductID = product.ProductID;
            newPurchaseOrderItem.Quantity = quantity;
            newPurchaseOrderItem.Weight = weight;
            newPurchaseOrderItem.Price = price;
            //newPurchaseOrderItem.PricingMethod = pricingMethod;

            if (productSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                newPurchaseOrderItem.Amount = newPurchaseOrderItem.Price * newPurchaseOrderItem.Quantity;
            else
                newPurchaseOrderItem.Amount = newPurchaseOrderItem.Price * newPurchaseOrderItem.Weight;
            //newPurchaseOrderItem.Amount = newPurchaseOrderItem.Price * newPurchaseOrderItem.PricingQuantity;
            newPurchaseOrderItem.PurchaseOrderID = this.ID;
            newPurchaseOrderItem.PurchaseOrder = this;

            newPurchaseOrderItem.ProductSkuInfo = ProductSkuInfo.Create(productSkuInfo.SkuID, productSkuInfo.Name, productSkuInfo.PricingMethod);

            Items.Add(newPurchaseOrderItem);
            this.TotalQuantity += newPurchaseOrderItem.Quantity;
            this.TotalAmount += newPurchaseOrderItem.Amount;
            this.TotalWeight += newPurchaseOrderItem.Weight;
        }
        public void RemoveItem(Guid itemID)
        {
            PurchaseOrderItem item = Items.FirstOrDefault(p => p.ID == itemID);
            if (item == null)
                throw new DomainException("无此ID");
            Items.Remove(item);
        }
        public void UpdateItem(Guid itemID, ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price)
        {
            PurchaseOrderItem item = this.Items.FirstOrDefault(p => p.ID == itemID);
            if (item != null)
            {
                item.PurchaseOrder = this;
                item.PurchaseOrderID = this.ID;
                //item.ProductID = product.ProductID;
                item.Price = price;
                item.ProductSkuInfo = productSkuInfo;
                this.TotalQuantity += (quantity - item.Quantity);
                item.Quantity = quantity;
                this.TotalWeight += (weight - item.Weight);
                item.Weight = weight;
                if (productSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                {
                    this.TotalAmount += (quantity * price - item.Amount);
                    item.Amount = item.Price * item.Quantity;
                }
                else
                {
                    this.TotalAmount += (weight * price - item.Amount);
                    item.Amount = item.Price * item.Weight;
                }
            }
            else
            {
                throw new DomainException("无此ID");
            }
        }
        public void UpdateItem(IEnumerable<PurchaseOrderItem> items)
        {
            var existingItems = this.Items.ToList();
            var updatedItems = items.Where(p => this.Items.Any(k => k.ID == p.ID)).ToList();
            var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
            var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
            deletedItems.ForEach(p => this.Items.Remove(p));
            addedItems.ForEach(p => this.AddItem(p.ProductSkuInfo, p.Quantity, p.Weight, p.Price));
            updatedItems.ForEach(p => this.UpdateItem(p.ID, p.ProductSkuInfo, p.Quantity, p.Weight, p.Price));
        }


    }
}
