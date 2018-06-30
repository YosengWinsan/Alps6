﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.ProductMgr
{
    public class ProductSku : EntityBase
    {
        //[Display(Name="SKU名")]
        //public string Name { get; set; }
        [Display(Name = "SKU全名")]
        public string FullName { get; set; }
        public Guid ProductID { get; set; }
        [Display(Name = "SKU描述")]
        public string Description { get; set; }
        [Display(Name = "Sku名")]
        public string Name { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }
        [Display(Name = "修改时间")]
        public DateTime ModifiedTime { get; set; }
        public virtual Product Product { get; set; }

        public static ProductSku Create(Guid productID, string name, string description, string fullName)
        {
            ProductSku sku = new ProductSku();
            sku.ProductID = productID;
            sku.Name = name;
            sku.Description = description;
            sku.FullName = fullName;
            sku.CreatedTime=DateTime.Now;
            return sku;
        }
        // public static ProductSku Create(Guid productID, string attributes, int stockQuantity, PricingMethod pricingMethod, decimal price)
        // {
        //     ProductSku sku = new ProductSku();
        //     sku.ProductID = productID;
        //     sku.Name = attributes;
        //     sku.FullName = attributes;
        //     sku.Description = attributes;
        //     sku.PricingMethod = pricingMethod;
        //     sku.UpdatePrice(price);
        //     sku.UpdateStockQuantity(stockQuantity, true);
        //     sku.CreatedTime = DateTime.Now;
        //     sku.ModifiedTime = sku.CreatedTime;
        //     return sku;
        // }
        // public static ProductSku Create(Product product, string attributes, int stockQuantity, PricingMethod pricingMethod, decimal price)
        // {
        //     ProductSku sku = new ProductSku();
        //     sku.ProductID = product.ID;
        //     sku.Name = attributes;
        //     //sku.FullName = product.Name + "-" + sku.Name;
        //     sku.Description = attributes;
        //     sku.PricingMethod = pricingMethod;
        //     sku.UpdatePrice(price);
        //     sku.UpdateStockQuantity(stockQuantity, true);
        //     sku.CreatedTime = DateTime.Now;
        //     sku.ModifiedTime = sku.CreatedTime;
        //     return sku;
        // }
        // public void UpdatePrice(decimal price)
        // {
        //     this.Price = price;
        // }
        // public void UpdateStockQuantity(int quantity, bool fullUpdate = false)
        // {
        //     if (fullUpdate)
        //     {
        //         if (quantity >= 0)
        //             this.StockQuantity = StockQuantity;
        //         else
        //             throw new DomainException("库存数量不能为负");
        //     }
        //     else
        //     {
        //         if (this.StockQuantity + quantity >= 0)
        //             this.StockQuantity = +quantity;
        //         else
        //             throw new DomainException("减少量超过库存数量");
        //     }
        // }

        // public ProductSkuInfo GetProductSkuInfo()
        // {
        //     return ProductSkuInfo.Create(this.ID, this.AttributeName, this.PricingMethod);
        // }
    }
}
