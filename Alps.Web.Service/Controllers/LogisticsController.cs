using Alps.Domain;
using Alps.Domain.LogisticsMgr;
using Alps.Web.Service.Extensions;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult GetCars()
        {
            var query = this._context.DispatchRecords.Where(p => p.Status == DispatchRecordStatus.Normal || p.Status == DispatchRecordStatus.InProcess)
            .Select(p => new { CarNumber = p.CarNumber, ID = p.ID });
            return this.AlpsActionOk(query.ToList());
        }
        [HttpGet("getDispatchRecord/{id}")]
        public IActionResult GetDispatchRecord(Guid id)
        {
            var query = this._context.DispatchRecords.Select(p => new DispatchRecordDto
            {
                ID = p.ID,CarNumber=p.CarNumber,
                Status = EnumHelper.GetDisplayValue(p.Status),Type=EnumHelper.GetDisplayValue(p.Type),
                GrossWeight=p.GrossWeight,
                TareWeight=p.TareWeight,
                GrossWeightOperator=p.GrossWeightOperator,
                TareWeightOperator=p.TareWeightOperator,
                GrossWeightTime=p.GrossWeightTime,
                TareWeightTime=p.TareWeightTime,
                WeightConfirmedOperator=p.WeightConfirmedOperator,
                WeightConfirmedTime=p.WeightConfirmedTime
                //WeightLists = p.WeightLists.Select(k => new WeightListDto { GrossWeight = k.GrossWeight, TareWeight = k.TareWeight })
            }).FirstOrDefault(p => p.ID == id);
            return this.AlpsActionOk(query);
        }

    }
}