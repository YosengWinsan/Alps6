using Alps.Domain;
using Alps.Domain.AccountingMgr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alps.Web.Service.Controllers
{
   [Authorize]
  [Route("api/[controller]")]
  public class TradeAccountsController : Controller
  {
    private readonly AlpsContext _context;
    public TradeAccountsController(AlpsContext context)
    {
      this._context = context;
    }
    // GET: api/values
    [HttpGet]
    public IEnumerable<TradeAccount> Get()
    {
      return _context.TradeAccounts;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public TradeAccount Get(Guid id)
    {
      return this._context.TradeAccounts.Find(id);
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
