using System;
using System.Collections.Generic;
using System.Linq;
using Alps.Domain.AccountingMgr;
using System.ComponentModel.DataAnnotations;
using Alps.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.ProductMgr
{
    public class DeliveryVoucher:EntityBase
    {
        [Display(Name = "制单人")]
        public string Creater { get; set; }
        [Display(Name = "制单时间")]
        [DisplayFormat(DataFormatString = "yyyy/mm/dd")]
        public DateTimeOffset CreateTime { get; set; }
        [Display(Name = "部门")]
        public Guid DepartmentID { get; set; }
        [Display(Name = "客户")]
        public Guid CustomerID { get; set; }

        [Display(Name = "经办人")]
        public string Handler { get; set; }
        [Display(Name = "单据状态")]
        public virtual DeliveryVoucherState State { get; set; }
        [Display(Name = "提交人")]
        public string SubmitUser { get; set; }
        [Display(Name = "总数量")]
         [Column(TypeName="decimal(18,2)")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "总重量")]
         [Column(TypeName="decimal(18,2)")]
        public decimal TotalWeight { get; set; }
        [Display(Name = "总金额")]
         [Column(TypeName="decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "明细")]
        public virtual ICollection<DeliveryVoucherItem> Items { get; set; }


        [Display(Name = "部门")]
        public virtual Department Department { get; set; }
        [Display(Name = "客户")]
        public virtual Customer Customer { get; set; }
        protected DeliveryVoucher()
        {
            Items = new HashSet<DeliveryVoucherItem>();
        }
        public static DeliveryVoucher Create(Guid departmentID, Guid customerID, string creater)
        {
            DeliveryVoucher newdeliveryVoucher = new DeliveryVoucher();
            newdeliveryVoucher.State = DeliveryVoucherState.Unconfirmed;
            newdeliveryVoucher.Creater = creater;
            newdeliveryVoucher.CreateTime = DateTime.Now;
            newdeliveryVoucher.DepartmentID = departmentID;
            newdeliveryVoucher.CustomerID = customerID;
            newdeliveryVoucher.SubmitUser = "";
            return newdeliveryVoucher;
        }
        public void AddItem(ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Position position, string remark = "", Guid? purchaseOrderItemID = null)
        {
            AddItem(productSkuInfo, quantity, weight, price, productNumber, position.ID, remark, purchaseOrderItemID);
        }
        public void AddItem(ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Guid positionID, string remark = "", Guid? purchaseOrderItemID = null)
        {
            if (productNumber == null)
                productNumber = string.Empty;
            DeliveryVoucherItem newDeliveryVoucherItem = new DeliveryVoucherItem();
            newDeliveryVoucherItem.DeliveryVoucher = this;
            newDeliveryVoucherItem.DeliveryVoucherID = this.ID;
            newDeliveryVoucherItem.Remark = remark;
            newDeliveryVoucherItem.ProductSkuInfo = productSkuInfo;
            newDeliveryVoucherItem.Quantity = quantity;
            newDeliveryVoucherItem.Weight = weight;
            newDeliveryVoucherItem.Price = price;
            if (productSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                newDeliveryVoucherItem.Amount = quantity * price;
            else
                newDeliveryVoucherItem.Amount = weight * price;
            newDeliveryVoucherItem.ProductNumber = productNumber;
            newDeliveryVoucherItem.PositionID = positionID;
            newDeliveryVoucherItem.PurchaseOrderItemID = purchaseOrderItemID;
            RefreshAmount(newDeliveryVoucherItem);
            Items.Add(newDeliveryVoucherItem);
        }
        public void RemoveItem(Guid itemID)
        {
            DeliveryVoucherItem item = Items.FirstOrDefault(p => p.ID == itemID);
            if (item == null)
                throw new DomainException("无此ID");
            Items.Remove(item);
        }
        public void UpdateItem(Guid itemID, ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Guid positionID, string remark = "", Guid? purchaseOrderItemID = null)
        {
            DeliveryVoucherItem item = this.Items.FirstOrDefault(p => p.ID == itemID);
            if (item != null)
            {
                if (productNumber == null)
                    productNumber = string.Empty;
                //item.Product = product;
                item.ProductSkuInfo = productSkuInfo;
                item.Remark = remark;
                item.Quantity = quantity;
                item.Weight = weight;
                item.ProductNumber = productNumber;
                item.PositionID = positionID;
                item.Price = price;
                //item.Amount = price * pricingQuantity;
                item.PurchaseOrderItemID = purchaseOrderItemID;

                RefreshAmount(item);
            }
            else
            {
                throw new DomainException("无此ID");
            }
        }
        private void RefreshAmount(DeliveryVoucherItem item)
        {
            this.TotalAmount = this.TotalAmount - item.Amount;
            if (item.ProductSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                item.Amount = item.Quantity * item.Price;
            else
                item.Amount = item.Weight * item.Price;
            this.TotalAmount = this.TotalAmount + item.Amount;
        }
        public void UpdateItem(IEnumerable<DeliveryVoucherItem> items)
        {
            var existingItems = this.Items.ToList();
            var updatedItems = items.Where(p => this.Items.Any(k => k.ID == p.ID)).ToList();
            var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
            var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
            deletedItems.ForEach(p => this.Items.Remove(p));
            addedItems.ForEach(p => this.AddItem(p.ProductSkuInfo, p.Quantity, p.Weight, p.Price, p.ProductNumber, p.PositionID, p.Remark, p.PurchaseOrderItemID));
            updatedItems.ForEach(p => this.UpdateItem(p.ID, p.ProductSkuInfo, p.Quantity, p.Weight, p.Price, p.ProductNumber, p.PositionID, p.Remark, p.PurchaseOrderItemID));

        }
        public void Submit(string submitter)
        {
            this.State = DeliveryVoucherState.Confirmed;
            this.SubmitUser = submitter;
        }
    }
}
