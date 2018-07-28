using System;
using System.ComponentModel.DataAnnotations;

namespace Alps.Domain.ProductMgr
{
    public class ProductSku : EntityBase
    {
        //TODO:增加SKU所属Department
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
        public Boolean Deleted { get; set; }
        public string Code { get; set; }
        public virtual Product Product { get; set; }

        //销售相关信息
        public bool Vendable { get; set; }
        public string CommodityName { get; set; }
        public decimal ListPrice { get; set; }
        public decimal QuantityRate { get; set; }
        //Quantity
        public decimal StockQuantity { get; set; }
        public decimal PreSellQuantity { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal SellableQuantity { get { return PreSellQuantity + StockQuantity - OrderedQuantity; } }
        //AuxiliaryQuantity
        public decimal StockAuxiliaryQuantity { get; set; }
        public decimal PreSellAuxiliaryQuantity { get; set; }
        public decimal OrderedAuxiliaryQuantity { get; set; }
        public decimal SellableAuxiliaryQuantity { get { return PreSellAuxiliaryQuantity + StockAuxiliaryQuantity - OrderedAuxiliaryQuantity; } }
        // public static ProductSku Create(Product product, string name, string description)
        // {
        //     return ProductSku.Create(product, name, description, "");
        // }
        // public static ProductSku Create(Product product, string name, string description, string code)
        // {
        //     ProductSku sku = new ProductSku();
        //     sku.Name = name;
        //     sku.Description = description;
        //     sku.CreatedTime = DateTime.Now;
        //     sku.Deleted = false;
        //     sku.Code = code;
        //     sku.UpdateProduct(product);
        //     return sku;
        // }
        public static ProductSku Create(Guid productID, string name, string description, string code, bool vendable)
        {
            return ProductSku.Create(productID,name,description,code,vendable,"",0,0,0,0);
        }
        public static ProductSku Create(Guid productID, string name, string description, string code, bool vendable, string commodityName, decimal listPrice, decimal quantityRate
        , decimal preSellQuantity, decimal preSellAuxiliaryQuantity)
        {
            ProductSku sku = new ProductSku();
            sku.ProductID = productID;
            sku.Name = name;
            sku.FullName = name;
            sku.Description = description;
            sku.Code = code;
            sku.CreatedTime = DateTime.Now;
            sku.Vendable = vendable;
            if (sku.Vendable)
            {
                sku.CommodityName=commodityName==string.Empty?name:commodityName;
                sku.ListPrice = listPrice;
                sku.QuantityRate = quantityRate;
                sku.PreSellQuantity = preSellQuantity;
                sku.PreSellAuxiliaryQuantity = preSellAuxiliaryQuantity;
            }
            else
            {
                sku.CommodityName = "";
                sku.ListPrice = 0;
                sku.QuantityRate = 1;
                sku.PreSellAuxiliaryQuantity = 0;
                sku.PreSellQuantity = 0;
            }
            return sku;
        }
        public void UpdateProduct(Product p)
        {
            this.ProductID = p.ID;
            this.FullName = p.Name + " " + this.Name;
        }
        public void MarkAsDeleted()
        {
            this.Deleted = true;
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
