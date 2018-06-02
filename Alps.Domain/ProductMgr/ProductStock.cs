using System;
using Alps.Domain.AccountingMgr;

namespace Alps.Domain.ProductMgr
{
    public class ProductStock:EntityBase
    {
        
        //public virtual Product Product { get; set; }
        //public Guid ProductID { get; set; }

        public Guid ProductSkuID { get; set; }
        public decimal AuxiliaryQuantity { get; set; }
        public decimal Quantity { get; set; }
        public Guid PositionID { get; set; }
        public Guid OwnerID { get; set; }
        public string SerialNumber { get; set; }

        public TradeAccount Owner { get; set; }
        public Position Position { get; set; }
        public ProductSku ProductSku { get; set; }
        //public virtual Department Department { get; set; }

        public static ProductStock Create(Guid ownerID, Guid positionID, Guid skuID, string serialNumber, decimal quantity, decimal auxiliaryQuantity)
        {
            ProductStock productStock=new ProductStock();
            productStock.OwnerID = ownerID;
            productStock.PositionID = positionID;
            productStock.ProductSkuID = skuID;
            productStock.SerialNumber = serialNumber;
            productStock.AuxiliaryQuantity = auxiliaryQuantity;
            productStock.Quantity = quantity;
            return productStock;
        }
        
    }
}
