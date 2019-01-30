using Alps.Domain;
using Alps.Domain.SaleMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Web.Service.Controllers
{

    [Authorize]
    [Produces("application/json")]
    [Route("api/Query")]
    public class QueryController : Controller
    {
        private readonly AlpsContext _context;
        public QueryController(AlpsContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("InitDatabase")]
        public IActionResult InitDatabase()
        {
            Alps.Domain.AlpsContext.Initial(_context);
            var addressService = new Alps.Domain.Service.AddressService(_context);
            foreach (var country in _context.Countries)
            {
                addressService.UpdateChildrenFullName(country);
            }
            (new Domain.Service.StockService(_context)).UpdateProductSkuQuantity();

            _context.SaveChanges();


            return Ok(true);
        }

        [HttpGet("DashboardInfo")]
        public IActionResult DashboardInfo()
        {
            var rst = new
            {
                CatagoryCount = _context.Catagories.Count(),
                ProductCount = _context.Products.Count(),
                ProductSkuCount = _context.ProductSkus.Count(),
                ProductStockCount = _context.ProductStocks.Count(),
                StockInCount = _context.StockInVouchers.Count(),
                StockOutCount = _context.StockOutVouchers.Count(),
                CommodityCount = _context.Commodities.Count(),
                SaleOrderCount = _context.SaleOrders.Count(),
                SaleOrderConfirmCount = _context.SaleOrders.Where(p => p.Status == SaleOrderStatus.Confirm).Count(),
            };
            return this.AlpsActionOk(rst);
        }
        [HttpGet("GetAnswer")]
        public IActionResult GetPath()
        {
            List<int> paths = new List<int>();
            paths.Add(11);
            return this.AlpsActionOk();
        }
                [HttpGet("SupplierOptions")]
        public IActionResult SupplierOptions()
        {
            var query = from sc in _context.SupplierClasses
                        from s in _context.Suppliers
                        where sc.ID == s.SupplierClassID
                        group new { sc, s } by sc into c
                        select new AlpsSelectorItemDto
                        {
                            Value = c.Key.ID,
                            DisplayValue = c.Key.Name,
                            IsOption = false,
                            Children = c.Select(p => new AlpsSelectorItemDto
                            {
                                Value = p.s.ID
,
                                DisplayValue = p.s.Name
                            })
                        };
            return Ok(query);
            // _context.SupplierClasses.Select(p=>new AlpsSelectorItemDto{Value=p.ID,DisplayValue=p.Name,IsOption=false,Children=_context.Suppliers.Select(
            //     l=>new AlpsSelectorItemDto{Value=l.}

            // )})
            // return Ok(_context.Suppliers.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("CustomerOptions")]
        public IActionResult CustomerOptions()
        {
            return this.AlpsActionOk(_context.Customers.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("ProductOptions")]
        public IActionResult ProductOptions()
        {
            return Ok(_context.Products.Where(p => !p.Deleted).Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.FullName }));
        }
        [HttpGet("ProductOption/{id}")]
        public IActionResult ProductOption(Guid id)
        {
            return Ok(_context.Products.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }).FirstOrDefault(p => p.Value == id));
        }
        [HttpGet("ProductSkuOptions")]
        public IActionResult ProductSkuOptions()
        {
            var unionQuery = _context.Products.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.CatagoryID, IsOption = false })
              .Union(_context.ProductSkus.Where(k => !k.Deleted).Select(p => new TreeNode { ID = p.ID, Name = p.FullName, ParentID = p.ProductID }))
              .Union(_context.Catagories.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ParentID, IsOption = false }));
            var catagories = BuildTree(unionQuery.ToList(), null);
            return Ok(catagories);
        }
        [HttpGet("ProductSkuOptionsForSale")]
        public IActionResult ProductSkuOptionsForSale()
        {
            var unionQuery = _context.Products.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.CatagoryID, IsOption = false })
              .Union(_context.ProductSkus.Where(k => !k.Deleted && k.Vendable).Select(p => new TreeNode { ID = p.ID, Name = p.FullName, ParentID = p.ProductID }))
              .Union(_context.Catagories.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ParentID, IsOption = false }));
            var catagories = BuildTree(unionQuery.ToList(), null);
            return Ok(catagories);
        }
        [HttpGet("CatagoryOptions")]
        public IActionResult CatagoryOptions()
        {
            var unionQuery = _context.Catagories.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ParentID });
            var catagories = BuildTree(unionQuery.ToList(), null);

            //var query=
            return Ok(catagories);
        }
        [HttpGet("CatagoryOption/{id}")]
        public IActionResult CatagoryOption(Guid id)
        {
            var unionQuery = _context.Catagories.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }).FirstOrDefault(p => p.Value == id);
            //var query=
            return Ok(unionQuery);
        }
        [HttpGet("CommodityOptions")]
        public IActionResult CommodityOptions()
        {
            return this.AlpsActionOk(_context.Commodities.Select(p => new { Value = p.ID, DisplayValue = p.Name, QuantityRate = p.QuantityRate, IsOption = true }));
        }
        [HttpGet("DispatchRecordOptions")]
        public IActionResult DispatchRecordOptions()
        {
            return this.AlpsActionOk(_context.DispatchRecords.Select(p => new { Value = p.ID, DisplayValue = p.CarNumber, IsOption = true }));
        }
        class TreeNode
        {
            public Guid ID { get; set; }
            public Guid? ParentID { get; set; }
            public string Name { get; set; }
            public bool IsOption { get; set; }
            public TreeNode()
            {
                IsOption = true;
            }
        }

        private IEnumerable<AlpsSelectorItemDto> BuildTree(IEnumerable<TreeNode> treeNodes, Guid? parentID)
        {
            List<AlpsSelectorItemDto> list = new List<AlpsSelectorItemDto>();
            var childTreeNodes = treeNodes.Where(p => p.ParentID == parentID);

            foreach (var treenode in childTreeNodes)
            {
                AlpsSelectorItemDto dto = new AlpsSelectorItemDto();
                dto.Value = treenode.ID;
                dto.DisplayValue = treenode.Name;
                dto.IsOption = treenode.IsOption;
                dto.Children = BuildTree(treeNodes, treenode.ID);
                list.Add(dto);
            }
            return list;

        }

        [HttpGet("DepartmentOptions")]
        public IActionResult DepartmentOptions()
        {
            return Ok(_context.Departments.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("PositionOptions")]
        public IActionResult PositionOptions()
        {
            return Ok(_context.Positions.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("TradeAccountOptions")]
        public IActionResult TradeAccountOptions([FromBody]int id)
        {
            //Where(p=>p.Types.Contains(type))
            return Ok(_context.TradeAccounts.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("CountyOptions")]
        public IActionResult CountyOptions()
        {
            var unionQuery = _context.Countries.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = null, IsOption = false })
            .Union(_context.Provinces.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.CountryID, IsOption = false }))
             .Union(_context.Cities.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ProvinceID, IsOption = false }))
              .Union(_context.Counties.Select(p => new TreeNode { ID = p.ID, Name = p.FullName, ParentID = p.CityID }));
            //Where(p=>p.Types.Contains(type))
            return this.AlpsActionOk(BuildTree(unionQuery.ToList(), null));
        }
        [HttpGet("LenderOptions")]
        public IActionResult LenderOptions()
        {
            //Where(p=>p.Types.Contains(type))
            return Ok(_context.Lenders.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
        [HttpGet("RoleOptions")]
        public IActionResult RoleOptions()
        {
            //Where(p=>p.Types.Contains(type))
            return Ok(_context.AlpsRoles.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
        }
    }
}
