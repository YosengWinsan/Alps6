using Alps.Domain;
using Alps.Domain.ProductMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Alps.Web.Service.Controllers
{
  [Produces("application/json")]
  [Route("api/Catagories")]
  public class CatagoriesController : Controller
  {
    private readonly AlpsContext _context;
    public CatagoriesController(AlpsContext context)
    {
      this._context = context;
    }
    // GET: api/Catagories
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }
    public class CatagoryPathDto
    {
      public Guid ID { get; set; }
      public string Name { get; set; }
      public Guid? ParentID { get; set; }
    }
    private IEnumerable<CatagoryPathDto> GetCatagoryPathByID(Guid? id)
    {
      var query = from c in this._context.Catagories
                  where c.ID == id
                  select new CatagoryPathDto { ID = c.ID, Name = c.Name, ParentID = c.ParentID };
      return
         query.ToList().SelectMany(p => GetCatagoryPathByID(p.ParentID)).Concat(query.ToList());

      //return query.ToList().Concat(query.ToList().SelectMany(t => GetCatagoryPathByID(t.ParentID)));

      // return this._context.Catagories.
    }
    [Route("getListByParentID/{id:guid?}")]
    [HttpGet]
    public ActionResult GetListByParentID(Guid? id)
    {
      return Ok(
        new
        {
          Path = GetCatagoryPathByID(id),
          data = this._context.Catagories.Where(p => p.ParentID == id).Select(p => new CatagoryListDto { Name = p.Name, ID = p.ID }
          )
        });
    }

    
     [Route("{id}")]
    // GET: api/Catagories/5
    [HttpGet()]
    public CatagoryEditDto Get(Guid id)
    { 

      var catagory=_context.Catagories.Select(p => new CatagoryEditDto { Name = p.Name, ID = p.ID,ParentID=p.ParentID }).FirstOrDefault(p=>p.ID==id);
      return catagory;
    }

    // POST: api/Catagories
    [HttpPost]
    public IActionResult Post([FromBody]CatagoryEditDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest();
      var catagory = Catagory.Create(dto.Name);
      if (dto.ParentID.HasValue && dto.ParentID.Value != Guid.Empty)
      {
        var parentCatagory=_context.Catagories.Find(dto.ParentID);
        parentCatagory.AddChildCatagory(catagory);
      }
      _context.Catagories.Add(catagory);
      _context.SaveChanges();
      return this.AlpsActionOk();
    }

    // PUT: api/Catagories/5
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody]CatagoryEditDto dto)
    {
      if (!ModelState.IsValid || id != dto.ID)
      {
        return BadRequest();
      }
      var catagory = _context.Catagories.Find(id);
      catagory.Name = dto.Name;
      if(dto.ParentID.HasValue && dto.ParentID.Value != Guid.Empty && catagory.ParentID!=dto.ParentID)
      {
        var parentCatagory = _context.Catagories.Find(dto.ParentID);
        parentCatagory.AddChildCatagory(catagory);
      }
      
      _context.SaveChanges();
      return this.AlpsActionOk();
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
