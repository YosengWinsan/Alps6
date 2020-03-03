using System;
using System.Linq;
using System.Threading.Tasks;
using Alps.Domain;
using Alps.Domain.LoanMgr;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alps.Web.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanVoucher2sController : ControllerBase
    {
        private readonly AlpsContext _context;

        public LoanVoucher2sController(AlpsContext context)
        {
            _context = context;
        }
        [HttpGet("getWaterBills")]
        public IActionResult GetWaterBills()
        {

            var billDtos = _context.LoanVoucher2s.SelectMany(p => p.Records).Where(p => p.CreateTime.Date == DateTimeOffset.Now.Date)
            .Select(p => new LoanRecordDto { ID = p.ID, Date = p.OperateTime, Name = p.LoanVoucher.Lender.Name, Amount = p.Amount, Interest = p.Interest, Type = p.Type });
            return this.AlpsActionOk(billDtos);
        }
        [HttpGet("getByHashCode/{hashCode}")]
        public IActionResult GetLoanVouchers([FromRoute]string hashCode)
        {
            return this.AlpsActionOk(_context.LoanVoucher2s.Where(p => (p.IdentityCode.Contains(hashCode) || p.Lender.Name.Contains(hashCode)) && p.Amount > 0)
            .Select(l => new LoanVoucherListDto()
            {
                ID = l.ID,
                Date = l.DepositTime,
                Amount = l.Amount,
                InterestRate = l.InterestRate,
                Lender = l.Lender.Name,
                InterestSettlable = LoanVoucher2.GetInterestDay(l.InterestSettlementDate, LoanVoucher2.GetSettlableDate()) >= 30 ? true : false
            }));
        }
        [HttpPost("getprintinfo")]
        public IActionResult GetPrintInfo([FromBody]PrintInfoRequest req)
        {
            var dto = _context.LoanVoucher2s.SelectMany(p => p.Records).Select(p => new PrintInfo
            {
                ID = p.ID,
                Name = p.LoanVoucher.Lender.Name,
                Date = p.OperateTime,
                Amount = p.Amount,
                InterestRate = p.LoanVoucher.InterestRate,
                Operator = p.Creater,
                Interest = p.Interest,
                VoucherNumber = p.LoanVoucher.VoucherNumber,
                MobilePhoneNumber = p.LoanVoucher.Lender.MobilePhoneNumber
            }).FirstOrDefault(p => p.ID == req.ID);
            return this.AlpsActionOk(dto);
        }

        [HttpGet("getloanvoucherinfo/{id}")]
        public IActionResult GetLoanVoucherInfo([FromRoute]Guid id)
        {

            LoanVoucherInfoDto dto = _context.LoanVoucher2s.Select(p => new LoanVoucherInfoDto
            {
                ID = p.ID,
                InterestDays = LoanVoucher2.GetInterestDay(p.InterestSettlementDate, DateTime.Now),
                Name = p.Lender.Name,
                Date = p.DepositTime,
                InterestSettlementDate = p.InterestSettlementDate,
                Amount = p.Amount,
                InterestRate = p.InterestRate,
                SettlableInterestDays = LoanVoucher.GetInterestDay(p.InterestSettlementDate, LoanVoucher.GetSettlableDate())
            }).FirstOrDefault(p => p.ID == id);
            dto.SettlableInterest = dto.SettlableInterestDays * dto.InterestRate * dto.Amount / 30;
            return this.AlpsActionOk(dto);
        }
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoanVoucher2 v = LoanVoucher2.Create(dto.LenderID, User.Identity.Name);// dto.Amount, 0.006m, dto.VoucherNumber, dto.Date);
            v.Deposit(dto.Date, dto.Amount, dto.Memo, User.Identity.Name);
            _context.LoanVoucher2s.Add(v);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(v.ID);
        }
        [HttpPost("settleInterest/{id}")]
        public async Task<IActionResult> SettleInterest([FromBody] SettleInterestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher2 v = _context.LoanVoucher2s.Include(p => p.Records).FirstOrDefault(p => p.ID == dto.LoanVoucherID);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            var r = v.SettleInterest(dto.OperateTime, dto.Memo, User.Identity.Name);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(r.ID);
        }
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher2 v = _context.LoanVoucher2s.Include(p => p.Records).FirstOrDefault(p => p.ID == dto.LoanVoucherID);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            var r = v.Withdraw(dto.OperateTime, dto.Amount, dto.Memo, User.Identity.Name);
            //_context.WithdrawRecords.Add(r);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(r.ID);
        }
        // GET: api/LoanVouchers/5
        [HttpPost("invalidvoucher")]
        public async Task<IActionResult> InvalidVoucher([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher v = _context.LoanVoucher2s.Include(p => p.Records).FirstOrDefault(p => p.ID == id);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            v.Invalid();
            await _context.SaveChangesAsync();
            return this.AlpsActionOk();
        }
        [HttpPost("invalidrecord")]
        public async Task<IActionResult> InvalidRecord([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher2 v = _context.LoanVoucher2s.Include(p => p.Records).FirstOrDefault(p => p.Records.Any(k => k.ID == id));
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            v.InvalidRecord(id);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk();
        }


        private bool LoanVoucherExists(Guid id)
        {
            return _context.LoanVouchers.Any(e => e.ID == id);
        }
    }

}