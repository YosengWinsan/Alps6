using Alps.Domain.ProductMgr;
namespace Alps.Domain.Service
{
    public class ProductService
    {
        AlpsContext _context;
        public ProductService(AlpsContext context)
        {
            this._context = context;
        }
  

    }
}