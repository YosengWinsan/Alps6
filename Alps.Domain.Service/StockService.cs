using Alps.Domain.ProductMgr;
using Alps.Domain.StockMgr;
using Alps.Domain;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alps.Domain.Service
{
    public class StockService
    {
        private readonly AlpsContext _context;

        public StockService(AlpsContext context)
        {
            this._context = context;
        }
        public void StockIn(StockInVoucher stockInVoucher)
        {
            if (stockInVoucher.Status != StockInVoucherStatus.Unsubmit)
                throw new DomainException("单据状态异常");
            stockInVoucher.Status = StockInVoucherStatus.Confirm;
            foreach (var item in stockInVoucher.Items)
            {
                ProductStockIn(stockInVoucher.DepartmentID, item.PositionID, item.ProductSkuID, item.SerialNumber, item.Quantity, item.AuxiliaryQuantity);
            }
        }
        public void StockOut(StockOutVoucher stockOutVoucher)
        {
            if (stockOutVoucher.Status != StockOutVoucherStatus.Unsubmit)
                throw new DomainException("单据状态异常");
            stockOutVoucher.Status = StockOutVoucherStatus.Confirm;
            foreach (var item in stockOutVoucher.Items)
            {
                ProductStockOut(stockOutVoucher.DepartmentID, item.PositionID, item.ProductSkuID, item.SerialNumber, item.Quantity, item.AuxiliaryQuantity);
            }

        }

        private void ProductStockIn(Guid ownerID, Guid positionID, Guid skuID, string serialNumber, decimal quantity, decimal auxiliaryQuantity)
        {
            ProductStock productStock = null;
            productStock = _context.ProductStocks.Find(ownerID, positionID, skuID, serialNumber);
            if (productStock == null)
            {
                ProductStock newProductStock = ProductStock.Create(ownerID, positionID, skuID, serialNumber, quantity, auxiliaryQuantity);
                _context.ProductStocks.Add(newProductStock);
            }
            else
            {
                if (serialNumber != string.Empty)
                {
                    throw new DomainException("已存在此编码");
                }
                else
                {
                    productStock.AuxiliaryQuantity += auxiliaryQuantity;
                    productStock.Quantity += quantity;
                }
            }
            var sku = _context.ProductSkus.Find(skuID);
            sku.StockAuxiliaryQuantity += auxiliaryQuantity;
            sku.StockQuantity += quantity;

        }
        private void ProductStockOut(Guid ownerID, Guid positionID, Guid skuID, string serialNumber, decimal quantity, decimal auxiliaryQuantity)
        {
            ProductStock productStock = null;
            productStock = _context.ProductStocks.Find(ownerID, positionID, skuID, serialNumber);
            if (productStock != null)
            {
                if (serialNumber != string.Empty)
                {
                    if (productStock.AuxiliaryQuantity == auxiliaryQuantity && productStock.Quantity == quantity)
                        _context.ProductStocks.Remove(productStock);
                    else
                        throw new DomainException("库存数量已变化");
                }
                else
                {
                    if (productStock.AuxiliaryQuantity >= auxiliaryQuantity && productStock.Quantity >= quantity)
                    {
                        productStock.Quantity -= quantity;
                        productStock.AuxiliaryQuantity -= auxiliaryQuantity;
                    }
                    else
                        throw new DomainException("库存量不足");
                    // productStock.AuxiliaryQuantity -=auxiliaryQuantity ;
                    // productStock.Quantity -= quantity;
                    if (productStock.AuxiliaryQuantity == 0 && productStock.Quantity == 0)
                        _context.ProductStocks.Remove(productStock);
                }
                var sku = _context.ProductSkus.Find(skuID);
                sku.StockAuxiliaryQuantity -= auxiliaryQuantity;
                sku.StockQuantity -= quantity;
            }
            else
            {
                throw new DomainException("无此库存");
            }
        }
        public void UpdateProductSkuQuantity()
        {
            var stocks = _context.ProductStocks.GroupBy(p => p.ProductSkuID).Select(p => new { ID = p.Key, Quantity = p.Sum(k => k.Quantity), AuxiliaryQuantity = p.Sum(k => k.AuxiliaryQuantity) });
            foreach (var stock in stocks)
            {
                var sku = _context.ProductSkus.Find(stock.ID);
                sku.StockQuantity = stock.Quantity;
                sku.StockAuxiliaryQuantity = stock.AuxiliaryQuantity;
            }
        }
        public string StockContext()
        {
            return _context.GetHashCode().ToString();
        }
    }
}
