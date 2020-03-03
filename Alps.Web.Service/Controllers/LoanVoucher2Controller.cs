using System;
using System.Linq;
using Alps.Domain;
using Alps.Web.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alps.Web.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    public class LoanVoucher2sController:ControllerBase
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
                InterestSettlable = LoanVoucher2.GetInterestDay(l.InterestSettlementDate, LoanVoucher.GetSettlableDate()) >= 30 ? true : false
            }));
        }
        [HttpPost("getprintinfo")]
        public IActionResult GetPrintInfo([FromBody]PrintInfoRequest req)
        {
            PrintInfo dto = null;
            if (req.Type == OperateType.Deposit)
                dto = _context.LoanVouchers.Where(p => p.ID == req.ID).Select(p => new PrintInfo
                {
                    Name = p.Lender.Name,
                    Date = p.DepositDate,
                    Amount = p.Amount,
                    InterestRate = p.InterestRate,
                    Operator = p.Operator,
                    Interest = 0,
                    VoucherNumber = p.VoucherNumber,
                    MobilePhoneNumber = p.Lender.MobilePhoneNumber
                }).FirstOrDefault();
            if (req.Type == OperateType.Withdraw || req.Type == OperateType.SettleInterest)
                dto = _context.LoanVouchers.SelectMany(p => p.WithdrawRecords).Where(p => p.ID == req.ID).Select(p => new PrintInfo
                {
                    Name = p.LoanVoucher.Lender.Name,
                    Date = p.Date,
                    Amount = p.Amount,
                    InterestRate = p.InterestRate,
                    Operator = p.Operator,
                    Interest = p.Interest
                }).FirstOrDefault();
            // dto = _context.WithdrawRecords.Where(p => p.ID == req.ID).Select(p => new PrintInfo
            // {
            //     Name = p.LoanVoucher.Lender.Name,
            //     Date = p.Date,
            //     Amount = p.Amount,
            //     InterestRate = p.InterestRate,
            //     Operator = p.Operator,
            //     Interest = p.Interest
            // }).FirstOrDefault();
            return this.AlpsActionOk(dto);
        }
        [HttpGet("getloanvoucherinfo/{id}")]
        public IActionResult GetLoanVoucherInfo([FromRoute]Guid id)
        {
            LoanVoucherInfoDto dto = _context.LoanVouchers.Select(p => new LoanVoucherInfoDto
            {
                ID = p.ID,
                InterestDays = LoanVoucher.GetInterestDay(p.InterestSettlementDate, DateTime.Now),
                Name = p.Lender.Name,
                Date = p.DepositDate,
                InterestSettlementDate = p.InterestSettlementDate,
                Amount = p.Amount,
                InterestRate = p.InterestRate
              ,
                SettlableInterestDays = LoanVoucher.GetInterestDay(p.InterestSettlementDate, LoanVoucher.GetSettlableDate())
            })
            .Where(p => p.ID == id).FirstOrDefault();
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
            LoanVoucher v = LoanVoucher.Create(dto.LenderID, dto.Amount, 0.006m, dto.VoucherNumber, dto.Date);
            _context.LoanVouchers.Add(v);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(v.ID);
        }
        [HttpPost("settleInterest/{id}")]
        public async Task<IActionResult> SettleInterest([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher v = _context.LoanVouchers.Include(p => p.WithdrawRecords).FirstOrDefault(p => p.ID == id);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            var r = v.InterestSettle();
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
            LoanVoucher v = _context.LoanVouchers.Include(p => p.WithdrawRecords).FirstOrDefault(p => p.ID == dto.LoanVoucherID);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            var r = v.Withdraw(dto.Amount);
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
            LoanVoucher v = _context.LoanVouchers.Include(p => p.WithdrawRecords).FirstOrDefault(p => p.ID == id);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            v.Invalid();
            await _context.SaveChangesAsync();
            return this.AlpsActionOk();
        }


        private bool LoanVoucherExists(Guid id)
        {
            return _context.LoanVouchers.Any(e => e.ID == id);
        }
    }
    
}