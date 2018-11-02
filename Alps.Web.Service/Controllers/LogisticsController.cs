using Alps.Domain;
using Alps.Domain.LogisticsMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace Alps.Web.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticsController : ControllerBase
    {
        private readonly AlpsContext _context;
        public LogisticsController(AlpsContext context)
        {
            this._context = context;
        }
        [HttpGet("getcars")]
        public IActionResult GetCars(){
            var query=this._context.DispatchRecords.Where(p=>p.Status==DispatchRecordStatus.Normal||p.Status==DispatchRecordStatus.InProcess)
            .Select(p=>p.CarNumber);
            return this.AlpsActionOk(query.ToList());
        }

    }
}