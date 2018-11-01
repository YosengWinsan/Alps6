using Alps.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCars(){
            
        }

    }
}