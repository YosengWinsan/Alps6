using Alps.Domain;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
  [Route("api/Query")]
  public class QueryController : Controller
  {
    private readonly AlpsContext _context;
    public QueryController(AlpsContext context)
    {
      _context = context;
    }
    [HttpGet("InitDatabase")]
    public IActionResult InitDatabase()
    {
      Alps.Domain.AlpsContext.Initial(_context);
      //_context.Database.Initialize(true);
      return Ok(true);
    }
        [HttpGet("TestOptions")]
        public IActionResult TestOptions()
        {
            return Ok( new { ValueType=1,Name= "Winsan"});
        }
        [HttpGet("SupplierOptions")]
    public IActionResult SupplierOptions()
    {
      return Ok(_context.Suppliers.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
    }
    [HttpGet("ProductOptions")]
    public IActionResult ProductOptions()
    {
      return Ok(_context.Products.Select(p => new AlpsSelectorItemDto { Value = p.ID, DisplayValue = p.Name }));
    }
    [HttpGet("ProductSkuOptions")]
    public IActionResult ProductSkuOptions()
    {
      var unionQuery = _context.Products.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.CatagoryID,IsOption=false })
        .Union(_context.ProductSkus.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ProductID }))
        .Union(_context.Catagories.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ParentID,IsOption=false }));
      var catagories = BuildTree(unionQuery.ToList(), null);
      
      //var query=
      return Ok(catagories);
    }
    [HttpGet("CatagoryOptions")]
    public IActionResult CatagoryOptions()
    {
      var unionQuery =_context.Catagories.Select(p => new TreeNode { ID = p.ID, Name = p.Name, ParentID = p.ParentID });
      var catagories = BuildTree(unionQuery.ToList(), null);
      
      //var query=
      return Ok(catagories);
    }
    [HttpGet("CommodityOptions")]
    public IActionResult CommodityOptions()
    {
       return this.AlpsActionOk(_context.Commodities.Select(p=>new AlpsSelectorItemDto{Value=p.ID,DisplayValue=p.Name}));
    }
    class TreeNode
    {
      public Guid ID { get; set; }
      public Guid? ParentID { get; set; }
      public string Name { get; set; }
      public bool IsOption{get;set;}
      public TreeNode(){
        IsOption=true;
      }
    }

    private IEnumerable< AlpsSelectorItemDto> BuildTree(IEnumerable<TreeNode> treeNodes,Guid? parentID)
    {
      List<AlpsSelectorItemDto> list = new List<AlpsSelectorItemDto>();
      var childTreeNodes = treeNodes.Where(p => p.ParentID == parentID);

      foreach(var treenode in childTreeNodes)
      {
        AlpsSelectorItemDto dto = new AlpsSelectorItemDto();
        dto.Value = treenode.ID;
        dto.DisplayValue = treenode.Name;
        dto.IsOption=treenode.IsOption;
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

  }
}
