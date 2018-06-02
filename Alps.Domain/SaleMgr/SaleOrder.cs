using System;
using System.Collections.Generic;
using System.Linq;
using Alps.Domain.Common;
using Alps.Domain.DistributionMgr;
using System.ComponentModel.DataAnnotations;
namespace Alps.Domain.SaleMgr
{
    public class SaleOrder : EntityBase
    {
        public Customer Customer { get; set; }
        [Display(Name="客户")]
        public Guid CustomerID { get; set; }
        [Display(Name="下单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name="订单状态")]
        public SaleOrderState State { get; set; }
        [Display(Name="配送地址")]
        public string DeliveryAddress { get; set; }
        public  ICollection<SaleOrderItem> Items { get; set; }
        public  ICollection<DistributionVoucher> DeliveryVouchers { get; set; }
        public SaleOrder ParentSaleOrder { get; set; }
        public SaleOrder()
        {
            Items = new HashSet<SaleOrderItem>();
            DeliveryVouchers = new HashSet<DistributionVoucher>();
        }

        public static SaleOrder Create(Guid customerID,SaleOrder parentSaleOrder = null)
        {
            SaleOrder saleOrder = new SaleOrder();
            saleOrder.CustomerID = customerID;
            saleOrder.Items = new HashSet<SaleOrderItem>();
            saleOrder.OrderTime = DateTime.Now;
            saleOrder.ParentSaleOrder = parentSaleOrder;
            return saleOrder;
        }

        public void UpdateItems(Guid commodityID, decimal count, decimal weight, Guid unitID, decimal price)
        {
            SaleOrderItem existingItem = this.Items.FirstOrDefault(p => p.CommodityID == commodityID);
            if (existingItem == null)
            {
                existingItem = new SaleOrderItem();
                this.Items.Add(existingItem);
            }
            existingItem.CommodityID = commodityID;
            existingItem.Quantity += new Quantity(count, weight);
            existingItem.UnitID = unitID;
            existingItem.Price = price;

            if (existingItem.Quantity.Count == 0)
            {
                this.Items.Remove(existingItem);
            }
            if (existingItem.Quantity.IsNegative())
                throw new DomainException("订单数量不能小于零");
        }
        
        public void Confirm()
        {
            this.State = SaleOrderState.Confirm;
        }
        public void AddItem(Guid commodityID,Quantity quantity,decimal price,string remark)
        {
            var item=new SaleOrderItem(){CommodityID=commodityID,Quantity=quantity,Price=price,Remark=remark};
            this.Items.Add(item);

        }
        public void RemoveItem(Guid commodityID,Quantity quantity)
        {
            var item=this.Items.FirstOrDefault(p => p.CommodityID == commodityID);
            if (item == null)
                throw new DomainException("订单中无此物品");
            this.Items.Remove(item);
            
        }
        private void UpdateItem(Guid itemID,Guid commodityID,Quantity quantity,decimal price,string remark)
        {
            if (itemID == Guid.Empty)
                throw new ArgumentException("参数不含主键");
            var existingSaleOrderItem = this.Items.FirstOrDefault(p => p.ID == itemID);
            if (existingSaleOrderItem == null)
                throw new DomainException("无此主键实体");
            existingSaleOrderItem.CommodityID = commodityID;
            existingSaleOrderItem.Quantity = quantity;
            existingSaleOrderItem.Price = price;
            existingSaleOrderItem.Remark = remark;
        }
        public void UpdateItems(IEnumerable<SaleOrderItem> items)
        {
            var existingItems = this.Items.ToList();
            var updatedItems = this.Items.Where(p => items.Any(k => k.ID == p.ID)).ToList();
            var addedItems = items.Where(p => !this.Items.Any(k => k.ID == p.ID)).ToList();
            var deletedItems = this.Items.Where(p => !items.Any(k => k.ID == p.ID)).ToList();
            deletedItems.ForEach(p => this.Items.Remove(p));
            addedItems.ForEach(p => this.AddItem(p.CommodityID,p.Quantity,p.Price,p.Remark));
            updatedItems.ForEach(p => this.UpdateItem(p.ID,p.CommodityID,p.Quantity,p.Price,p.Remark));
        }
        public void UpdateBy(SaleOrder saleOrder)
        {
            this.CustomerID = saleOrder.CustomerID;
            this.DeliveryAddress = saleOrder.DeliveryAddress;
            this.UpdateItems(saleOrder.Items);
        }
    }
}
