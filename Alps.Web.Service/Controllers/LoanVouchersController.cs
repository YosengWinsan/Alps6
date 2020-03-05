using System;
using System.Collections;
using System.Collections.Generic;
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
    public class LoanVouchersController : ControllerBase
    {
        private readonly AlpsContext _context;

        public LoanVouchersController(AlpsContext context)
        {
            _context = context;
        }
        private IQueryable<LoanVoucher> GetLoanVoucher()
        {
            return _context.LoanVouchers.Include(p => p.Records);

        }
        private LoanVoucherDetailDto MapToDetailDto(LoanVoucher loanVoucher)
        {
            LoanVoucherDetailDto dto = new LoanVoucherDetailDto();
            dto.OperateTime = loanVoucher.DepositTime;
            dto.Lender = loanVoucher.Lender.Name;
            dto.Amount = loanVoucher.Amount;
            dto.DepositAmount = loanVoucher.DepositAmount;
            dto.Isinvalid = loanVoucher.IsInvalid;
            dto.Records = loanVoucher.Records.Select(p => new LoanRecordDto { IsInvalid = p.IsInvalid, LoanVoucherID = p.LoanVoucherID, ID = p.ID, Date = p.OperateTime, Name = p.LoanVoucher.Lender.Name, Amount = p.Amount, Interest = p.Interest, Type = p.Type }).ToList();
            return dto;
        }
        [HttpGet("getloanvoucherdetail/{id}")]
        public IActionResult GetLoanVoucherDetail([FromRoute]Guid id)
        {
            return this.AlpsActionOk(this.MapToDetailDto(this.GetLoanVoucher().Include(p => p.Lender).FirstOrDefault(p => p.ID == id)));
        }
        // [HttpGet("getloanvoucherdetailbyrecordID/{id}")]
        // public IActionResult GetLoanVoucherDetailByRecordID([FromRoute]Guid id)
        // {
        //     return this.AlpsActionOk(this.MapToDetailDto(this.GetLoanVoucher().FirstOrDefault(p => p.Records.Any(k => k.ID == id))));
        // }


        [HttpGet("getWaterBills")]
        public IActionResult GetWaterBills()
        {

            var billDtos = _context.LoanVouchers.SelectMany(p => p.Records).Where(p => p.CreateTime.Date == DateTimeOffset.Now.Date)
            .Select(p => new LoanRecordDto { IsInvalid = p.IsInvalid, LoanVoucherID = p.LoanVoucherID, ID = p.ID, Date = p.OperateTime, Name = p.LoanVoucher.Lender.Name, Amount = p.Amount, Interest = p.Interest, Type = p.Type });
            return this.AlpsActionOk(billDtos);
        }
        [HttpGet("getByHashCode/{hashCode}")]
        public IActionResult GetLoanVouchers([FromRoute]string hashCode)
        {
            return this.AlpsActionOk(_context.LoanVouchers.Where(p => (p.IdentityCode.Contains(hashCode) || p.Lender.Name.Contains(hashCode)) && p.Amount > 0)
            .Select(l => new LoanVoucherListDto()
            {
                ID = l.ID,
                Date = l.DepositTime,
                Amount = l.Amount,
                InterestRate = l.InterestRate,
                Lender = l.Lender.Name,
                InterestSettlable = LoanVoucher.GetInterestDay(l.InterestSettlementDate, LoanVoucher.GetSettlableDate()) >= 30 ? true : false
            }));
        }
        [HttpPost("getprintinfo")]
        public IActionResult GetPrintInfo([FromBody]PrintInfoRequest req)
        {
            var dto = _context.LoanVouchers.SelectMany(p => p.Records).Select(p => new PrintInfo
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

            LoanVoucherInfoDto dto = _context.LoanVouchers.Select(p => new LoanVoucherInfoDto
            {
                ID = p.ID,
                InterestDays = LoanVoucher.GetInterestDay(p.InterestSettlementDate, DateTime.Now),
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

            LoanVoucher v = LoanVoucher.Create(dto.LenderID, User.Identity.Name);// dto.Amount, 0.006m, dto.VoucherNumber, dto.Date);
            v.Deposit(dto.Date, dto.Amount, dto.Memo, User.Identity.Name);
            _context.LoanVouchers.Add(v);
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
            LoanVoucher v = _context.LoanVouchers.Include(p => p.Records).FirstOrDefault(p => p.ID == dto.LoanVoucherID);
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
            LoanVoucher v = _context.LoanVouchers.Include(p => p.Records).FirstOrDefault(p => p.ID == dto.LoanVoucherID);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            var r = v.Withdraw(dto.OperateTime, dto.Amount, dto.Memo, User.Identity.Name);
            //_context.WithdrawRecords.Add(r);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(r.ID);
        }
        // GET: api/LoanVouchers/5
        // [HttpPost("invalidvoucher")]
        // public async Task<IActionResult> InvalidVoucher([FromRoute] Guid id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     LoanVoucher2 v = _context.LoanVoucher2s.Include(p => p.Records).FirstOrDefault(p => p.ID == id);
        //     if (v == null)
        //         return this.AlpsActionWarning("无此ID");
        //     v.Invalid();
        //     await _context.SaveChangesAsync();
        //     return this.AlpsActionOk();
        // }
        [HttpPost("invalidrecord/{id}")]
        public async Task<IActionResult> InvalidRecord([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher v = _context.LoanVouchers.Include(p => p.Records).FirstOrDefault(p => p.Records.Any(k => k.ID == id));
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            v.InvalidRecord(id, User.Identity.Name);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk();
        }
        private bool LoanVoucherExists(Guid id)
        {
            return _context.LoanVouchers.Any(e => e.ID == id);
        }
        [HttpGet("getloaninterestrates")]
        public IActionResult GetInterestRates()
        {
            return this.AlpsActionOk(_context.LoanSettings.SelectMany(p => p.InterestRates));
        }
        [HttpPost("publishnewrate")]
        public async Task<IActionResult> PublishNewRate([FromBody] InterestRatePublishDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanSetting setting = DoGetLoanSetting();
            setting.PublishInterestRate(dto.StartExecutionDate, dto.Rate, User.Identity.Name);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(setting.InterestRates);
        }
        [HttpPost("saveloansetting")]
        public async Task<IActionResult> SaveLoanSetting([FromBody] InterestRatePublishDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanSetting setting = DoGetLoanSetting();
            setting.PublishInterestRate(dto.StartExecutionDate, dto.Rate, User.Identity.Name);
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(setting.InterestRates);
        }
        [HttpGet("getloansetting")]
        public IActionResult GetLoanSetting()
        {
            return this.AlpsActionOk(DoGetLoanSetting());
        }

        private LoanSetting DoGetLoanSetting()
        {
            LoanSetting setting = _context.LoanSettings.Include(p => p.InterestRates).FirstOrDefault();
            if (setting == null)
                setting = LoanSetting.Create(0, 0);
            _context.SaveChanges();
            return setting;
        }
        [HttpPost("importlender")]
        public async Task<IActionResult> ImportLender([FromBody]ICollection<LenderEditDto> list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Lenders.RemoveRange(_context.Lenders);
            foreach (var dto in list)
            {
                _context.Lenders.Add(Lender.Create(dto.Name, dto.IDNumber, dto.Memo));

            }
            var rst = await _context.SaveChangesAsync();
            return this.AlpsActionOk(rst);
        }
        [HttpPost("importvoucher")]
        public async Task<IActionResult> ImportLoanVoucher([FromBody]ICollection<VoucherImportDto> list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var dto in list)
            {
                Lender lender = _context.Lenders.FirstOrDefault(p => p.Name == dto.Lender);
                if (lender == null)
                    return this.AlpsActionError("找不到存款人:" + dto.Lender);
                //throw new DomainException("找不到存款人:" + dto.Lender);
                LoanVoucher v = LoanVoucher.Create(lender.ID, User.Identity.Name);// dto.Amount, 0.006m, dto.VoucherNumber, dto.Date);
                v.Deposit(dto.Date, dto.Amount, dto.Memo, User.Identity.Name);
                v.ReWriteVoucher(dto.ReWriteTime);
                _context.LoanVouchers.Add(v);
            }
            var rst = await _context.SaveChangesAsync();
            return this.AlpsActionOk(rst);
        }
        [HttpPost("importwithdraw")]
        public async Task<IActionResult> ImportWithdraw([FromBody]ICollection<WithdrawImportDto> list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var dto in list)
            {
                LoanVoucher v = _context.LoanVouchers.FirstOrDefault(p => p.Lender.Name == dto.Lender && p.DepositTime == dto.DepositDate && p.Amount == dto.DepositAmount);
                if (v == null)
                    return this.AlpsActionError("找不到" + dto.Lender + "在" + dto.DepositDate.ToString() + "的" + dto.DepositAmount.ToString() + "元的条子");
                v.Withdraw(dto.Date, dto.Amount, "", User.Identity.Name);
            }
            var rst = await _context.SaveChangesAsync();
            return this.AlpsActionOk(rst);
        }
        [HttpGet("getloanvouchersummary")]
        public IActionResult GetLoanVoucherSummary()
        {
            return this.AlpsActionOk(_context.LoanVouchers.Where(p => !p.IsInvalid && p.Amount > 0).GroupBy(p => p.Lender.Name)
            .Select(p => new VoucherSummaryDto { Lender = p.Key, TotalAmount = p.Sum(l => l.Amount), Count = p.Count() }));

        }
    }

}