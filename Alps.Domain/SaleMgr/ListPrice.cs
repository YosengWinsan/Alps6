using Alps.Domain.ProductMgr;
namespace Alps.Domain.SaleMgr
{
    public class ListPrice : EntityBase
    {
        public Commodity Commodity { get; set; }
        public decimal Price { get; set; }

    }
}
