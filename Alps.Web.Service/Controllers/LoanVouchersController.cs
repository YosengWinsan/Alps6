using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alps.Domain;
using Alps.Domain.LoanMgr;
using Alps.Web.Service.Model;

namespace Alps.Web.Service.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanVouchersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public LoanVouchersController(AlpsContext context)
        {
            _context = context;
        }

        [HttpGet("getByHashCode/{hashCode}")]
        public IActionResult GetLoanVouchers([FromRoute]string hashCode)
        {
            var settlableDate = LoanVoucher.GetSettlableDate();
            return this.AlpsActionOk(_context.LoanVouchers.Where(p => (p.HashCode.Contains(hashCode) || p.Lender.Name.Contains(hashCode)) && p.Amount > 0)
            .Select(l => new LoanVoucherListDto()
            {
                ID = l.ID,
                Date = l.DepositDate,
                Amount = l.Amount,
                InterestRate = l.InterestRate,
                Lender = l.Lender.Name
            ,
                InterestSettlable = LoanVoucher.GetInterestDay(l.InterestSettlementDate, LoanVoucher.GetSettlableDate()) >= 30 ? true : false
            }));
        }
        [HttpGet("getWaterBills")]
        public IActionResult GetWaterBills()
        {
            return this.AlpsActionOk(((_context.LoanVouchers.Where(p => p.ModifyDate.Date == DateTime.Now.Date)
            .Select(l => new WaterBillDto() { ID = l.ID, Date = l.DepositDate, Name = l.Lender.Name, Amount = l.Amount, InterestRate = l.InterestRate, Type = OperateType.Deposit }))
            .Union(_context.WithdrawRecords.Where(p => p.ModifyDate.Date == DateTime.Now.Date && p.Amount > 0)
            .Select(l => new WaterBillDto() { ID = l.ID, Date = l.Date, Name = l.LoanVoucher.Lender.Name, Amount = l.Amount, InterestRate = l.InterestRate, Interest = l.Interest, Type =OperateType.Withdraw }))
            .Union(_context.WithdrawRecords.Where(p => p.ModifyDate.Date == DateTime.Now.Date && p.Amount == 0)
            .Select(l => new WaterBillDto() { ID = l.ID, Date = l.Date, Name = l.LoanVoucher.Lender.Name, Amount = l.Amount, InterestRate = l.InterestRate, Interest = l.Interest, Type = OperateType.SettleInterest })))
            .OrderBy(p => p.Date)
            );
        }
        public class PrintInfoRequest{
            public OperateType Type{get;set;}
            public Guid ID{get;set;}
        }
        [HttpPost("getprintinfo")]
        public IActionResult GetPrintInfo([FromBody]PrintInfoRequest req)
        {
            PrintInfo dto = null;
            if (req.Type == OperateType.Deposit)
                dto = _context.LoanVouchers.Where(p=>p.ID==req.ID).Select(p => new PrintInfo
                {
                    Name = p.Lender.Name,
                    Date = p.DepositDate,
                    Amount = p.Amount,
                    InterestRate = p.InterestRate,
                    Operator = p.Operator,
                    Interest=0
                }).FirstOrDefault();
            if (req.Type == OperateType.Withdraw || req.Type==OperateType.SettleInterest)
                dto = _context.WithdrawRecords.Where(p=>p.ID==req.ID).Select(p => new PrintInfo
                {
                    Name = p.LoanVoucher.Lender.Name,
                    Date = p.Date,
                    Amount = p.Amount,
                    InterestRate = p.InterestRate,
                    Operator = p.Operator,
                    Interest=p.Interest
                }).FirstOrDefault();
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
            var r=v.InterestSettle();
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
            var r=v.Withdraw(dto.Amount);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(r.ID);
        }
        // GET: api/LoanVouchers/5



        private bool LoanVoucherExists(Guid id)
        {
            return _context.LoanVouchers.Any(e => e.ID == id);
        }
    }
}