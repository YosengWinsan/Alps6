﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alps.Domain.Common;
using Alps.Domain.LogisticsMgr;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alps.Domain.SaleMgr
{
    public class SaleOrder : EntityBase
    {
        public Customer Customer { get; set; }
        [Display(Name = "客户")]
        public Guid CustomerID { get; set; }
        [Display(Name = "下单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "订单状态")]
        public SaleOrderStatus Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalQuantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAuxiliaryQuantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "配送地址")]
        public string DeliveryAddress { get; set; }
        public ICollection<SaleOrderItem> Items { get; set; }
        public ICollection<DistributionVoucher> DeliveryVouchers { get; set; }
        public SaleOrder ParentSaleOrder { get; set; }
        public SaleOrder()
        {
            Items = new HashSet<SaleOrderItem>();
            DeliveryVouchers = new HashSet<DistributionVoucher>();
        }

        public static SaleOrder Create(Guid customerID, SaleOrder parentSaleOrder = null)
        {
            SaleOrder saleOrder = new SaleOrder();
            saleOrder.CustomerID = customerID;
            saleOrder.Items = new HashSet<SaleOrderItem>();
            saleOrder.OrderTime = DateTime.Now;
            saleOrder.ParentSaleOrder = parentSaleOrder;
            return saleOrder;
        }

        public void Confirm()
        {
            this.Status = SaleOrderStatus.Confirm;
        }
        public void AddItem(Guid productSkuID, string commodityName, decimal quantity, decimal price, decimal auxiliaryQuantity, string remark)
        {
            var item = new SaleOrderItem()
            {
                ProductSkuID = productSkuID,
                Quantity = quantity,
                Price = price,
                Remark = remark,
                AuxiliaryQuantity = auxiliaryQuantity,
                CommodityName = commodityName
            };
            this.Items.Add(item);
            this.TotalAuxiliaryQuantity += item.AuxiliaryQuantity;
            this.TotalQuantity += item.Quantity;
            this.TotalAmount += item.Amount;
        }
        public void RemoveItem(Guid itemID)
        {
            var item = this.Items.FirstOrDefault(p => p.ID == itemID);
            if (item == null)
                throw new DomainException("订单中无此物品");
            this.Items.Remove(item);
            this.TotalAuxiliaryQuantity -= item.AuxiliaryQuantity;
            this.TotalQuantity -= item.Quantity;
            this.TotalAmount -= item.Amount;
        }
        private void UpdateItem(Guid itemID, Guid productSkuID, string commodityName, decimal quantity, decimal price, decimal auxiliaryQuantity, string remark)
        {
            if (itemID == Guid.Empty)
                throw new ArgumentException("参数不含主键");
            var existingSaleOrderItem = this.Items.FirstOrDefault(p => p.ID == itemID);
            if (existingSaleOrderItem == null)
                throw new DomainException("无此主键实体");

            existingSaleOrderItem.ProductSkuID = productSkuID;
            existingSaleOrderItem.CommodityName = commodityName;
            existingSaleOrderItem.Remark = remark;
            if (existingSaleOrderItem.Quantity != quantity || existingSaleOrderItem.Price != price)
            {
                this.TotalQuantity -= existingSaleOrderItem.Quantity;
                this.TotalAmount -= existingSaleOrderItem.Amount;
                existingSaleOrderItem.Quantity = quantity;
                this.TotalQuantity += existingSaleOrderItem.Quantity;
                this.TotalAmount += existingSaleOrderItem.Amount;
            }
            if (existingSaleOrderItem.AuxiliaryQuantity != auxiliaryQuantity)
            {
                this.TotalAuxiliaryQuantity -= auxiliaryQuantity;
                existingSaleOrderItem.AuxiliaryQuantity = auxiliaryQuantity;
                this.TotalAuxiliaryQuantity += existingSaleOrderItem.AuxiliaryQuantity;
            }

        }
        public void UpdateItems(IEnumerable<ISaleOrderItem> items)
        {
            var existingItems = this.Items.ToList();
            var updatedItems = items.Where(p => this.Items.Any(k => k.ID == p.ID)).ToList();
            var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
            var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
            deletedItems.ForEach(p => this.Items.Remove(p));
            addedItems.ForEach(p => this.AddItem(p.ProductSkuID, p.CommodityName, p.Quantity, p.Price, p.AuxiliaryQuantity, p.Remark));
            updatedItems.ForEach(p => this.UpdateItem(p.ID, p.ProductSkuID, p.CommodityName, p.Quantity, p.Price, p.AuxiliaryQuantity, p.Remark));
            //this.UpdateTotalAmount();
        }

        // public void UpdateBy(SaleOrder saleOrder)
        // {
        //     this.CustomerID = saleOrder.CustomerID;
        //     this.DeliveryAddress = saleOrder.DeliveryAddress;
        //     this.UpdateItems(saleOrder.Items);
        // }
    }
    public interface ISaleOrderItem
    {
        Guid ID { get; set; }
        Guid ProductSkuID { get; set; }
        string CommodityName { get; set; }
        decimal Price { get; set; }
        string Remark { get; set; }
        decimal Quantity { get; set; }
        decimal AuxiliaryQuantity { get; set; }
    }
}
