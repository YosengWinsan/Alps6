using Alps.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alps.Domain.ProductMgr
{
    public class MaterialReceipt:EntityBase
    {
        [Display(Name = "给料部门")]
        public Guid SourceDepartmentID { get; set; }
        [Display(Name = "仓储部门")]
        public Guid DepartmentID { get; set; }
        [Display(Name = "制单人")]
        public string Creater { get; set; }
        [Display(Name = "制单时间")]
        public DateTimeOffset CreateTime { get; set; }
        [Display(Name = "提交人")]
        public string SubmitUser { get; set; }
        [Display(Name = "经办人")]
        public string Handler { get; set; }
        [Display(Name = "总数量")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "总重量")]
        public decimal TotalWeight { get; set; }
        [Display(Name = "总金额")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "单据状态")]
        public MaterialReceiptState State { get; set; }


        public virtual Department SourceDepartment { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = "明细")]
        public virtual ICollection<MaterialReceiptItem> Items { get; set; }
        protected MaterialReceipt()
        {
            Items = new HashSet<MaterialReceiptItem>();
        }
        public static MaterialReceipt Create(Guid departmentID, Guid sourceDepartmentID, string creater)
        {
            MaterialReceipt newMaterialReceipt = new MaterialReceipt();
            newMaterialReceipt.State = MaterialReceiptState.Unconfirmed;
            newMaterialReceipt.Creater = creater;
            newMaterialReceipt.CreateTime = DateTime.Now;
            newMaterialReceipt.DepartmentID = departmentID;
            newMaterialReceipt.SourceDepartmentID = sourceDepartmentID;
            newMaterialReceipt.SubmitUser = "";
            return newMaterialReceipt;
        }
        public void AddItem(ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Position position, string remark = "", Guid? purchaseOrderItemID = null)
        {
            AddItem(productSkuInfo, quantity, weight, price, productNumber, position.ID, remark, purchaseOrderItemID);
        }
        public void AddItem(ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Guid positionID, string remark = "", Guid? purchaseOrderItemID = null)
        {
            if (productNumber == null)
                productNumber = string.Empty;
            MaterialReceiptItem newMaterialReceiptItem = new MaterialReceiptItem();
            newMaterialReceiptItem.MaterialReceipt = this;
            newMaterialReceiptItem.MaterialReceiptID = this.ID;
            newMaterialReceiptItem.Remark = remark;
            newMaterialReceiptItem.ProductSkuInfo = productSkuInfo;
            newMaterialReceiptItem.Quantity = quantity;
            newMaterialReceiptItem.Weight = weight;
            newMaterialReceiptItem.Price = price;
            if (productSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                newMaterialReceiptItem.Amount = quantity * price;
            else
                newMaterialReceiptItem.Amount = weight * price;
            newMaterialReceiptItem.ProductNumber = productNumber;
            newMaterialReceiptItem.PositionID = positionID;
            RefreshAmount(newMaterialReceiptItem);
            Items.Add(newMaterialReceiptItem);
        }
        public void RemoveItem(Guid itemID)
        {
            MaterialReceiptItem item = Items.FirstOrDefault(p => p.ID == itemID);
            if (item == null)
                throw new DomainException("无此ID");
            Items.Remove(item);
        }
        public void UpdateItem(Guid itemID, ProductSkuInfo productSkuInfo, decimal quantity, decimal weight, decimal price, string productNumber, Guid positionID, string remark = "")
        {
            MaterialReceiptItem item = this.Items.FirstOrDefault(p => p.ID == itemID);
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
                RefreshAmount(item);
            }
            else
            {
                throw new DomainException("无此ID");
            }
        }
        private void RefreshAmount(MaterialReceiptItem item)
        {
            this.TotalAmount = this.TotalAmount - item.Amount;
            if (item.ProductSkuInfo.PricingMethod == PricingMethod.PricingByQuantity)
                item.Amount = item.Quantity * item.Price;
            else
                item.Amount = item.Weight * item.Price;
            this.TotalAmount = this.TotalAmount + item.Amount;
        }
        public void UpdateItem(IEnumerable<MaterialReceiptItem> items)
        {
            var existingItems = this.Items.ToList();
            var updatedItems = items.Where(p => this.Items.Any(k => k.ID == p.ID)).ToList();
            var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
            var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
            deletedItems.ForEach(p => this.Items.Remove(p));
            addedItems.ForEach(p => this.AddItem(p.ProductSkuInfo, p.Quantity, p.Weight, p.Price, p.ProductNumber, p.PositionID, p.Remark));
            updatedItems.ForEach(p => this.UpdateItem(p.ID, p.ProductSkuInfo, p.Quantity, p.Weight, p.Price, p.ProductNumber, p.PositionID, p.Remark));

        }
        public void Submit(string submitter)
        {
            this.State = MaterialReceiptState.Confirmed;
            this.SubmitUser = submitter;
        }
    }
}
