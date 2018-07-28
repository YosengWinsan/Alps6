using System;
using System.Linq;
using Alps.Domain.SaleMgr;
using Microsoft.EntityFrameworkCore;

namespace Alps.Domain.Service
{
    public class SaleService
    {
        private readonly AlpsContext _context;
        public SaleService(AlpsContext context)
        {
            this._context = context;
        }
        public void SubmitSaleOrder(Guid id)
        {
            var saleOrder = _context.SaleOrders.Include(p => p.Items).FirstOrDefault(p => p.ID == id);
            if (saleOrder != null)
            {
                if (saleOrder.Status != SaleOrderStatus.UnConfirm)
                {
                    throw new DomainException("单据已提交");
                }
                foreach (var item in saleOrder.Items)
                {
                    var sku = _context.ProductSkus.Find(item.ProductSkuID);
                    if (sku.SellableAuxiliaryQuantity < item.AuxiliaryQuantity && sku.SellableQuantity < item.Quantity)
                    {

                        throw new DomainException("数量不足");
                    }
                    else
                    {
                        sku.OrderedAuxiliaryQuantity += item.AuxiliaryQuantity;
                        sku.OrderedQuantity += item.Quantity;

                    }
                }
                saleOrder.Status = SaleOrderStatus.Confirm;
            }
            else
                throw new DomainException("无此订单");
        }
    }
}