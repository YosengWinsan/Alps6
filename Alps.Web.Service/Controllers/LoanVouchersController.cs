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
            var interestRates = GetInterestRates();
            return this.AlpsActionOk(_context.LoanVouchers.Where(p => (p.IdentityCode.Contains(hashCode) || p.Lender.Name.Contains(hashCode)) && p.Amount > 0)
            .Select(l => new LoanVoucherListDto()
            {
                ID = l.ID,
                Date = l.DepositTime,
                Amount = l.Amount,
                Interest = l.CalculateVoucherInterest(interestRates),
                Lender = l.Lender.Name,
                InterestSettlable = LoanVoucher.GetInterestDay(l.InterestSettlementDate, LoanVoucher.GetSettlableDate()) >= 30 ? true : false,

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
            var interestRates = _context.LoanSettings.SelectMany(p => p.InterestRates);
            var loanVoucher = _context.LoanVouchers.Include(p => p.Lender).FirstOrDefault(p => p.ID == id);
            LoanVoucherInfoDto dto = new LoanVoucherInfoDto
            {
                ID = loanVoucher.ID,
                InterestDays = LoanVoucher.GetInterestDay(loanVoucher.InterestSettlementDate, DateTime.Now),
                Name = loanVoucher.Lender.Name,
                Date = loanVoucher.DepositTime,
                InterestSettlementDate = loanVoucher.InterestSettlementDate,
                Amount = loanVoucher.Amount,
                InterestRate = loanVoucher.InterestRate,
                SettlableInterestDays = LoanVoucher.GetInterestDay(loanVoucher.InterestSettlementDate, LoanVoucher.GetSettlableDate()),
                SettlableInterest = loanVoucher.CalculateVoucherInterest(interestRates.ToList())
            };
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
        public async Task<IActionResult> SettleInterest([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanVoucher v = _context.LoanVouchers.Include(p => p.Records).FirstOrDefault(p => p.ID == id);
            if (v == null)
                return this.AlpsActionWarning("无此ID");
            if (v.CanSettleInterest())
            {
                var interestRates = GetInterestRates();
                v.SettleInterest(DateTimeOffset.Now, "", User.Identity.Name, interestRates);
                await _context.SaveChangesAsync();
            }
            return this.AlpsActionOk();
        }
        private IList<InterestRate> GetInterestRates()
        {
            return _context.LoanSettings.SelectMany(p => p.InterestRates).ToList();
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
            if (v.Amount < dto.Amount)
                return this.AlpsActionError("取款金额超过存款金额");
            var r = v.Withdraw(dto.OperateTime, dto.Amount, dto.Memo, User.Identity.Name, GetInterestRates());
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(r.ID);
        }

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
        // [HttpGet("getloaninterestrates")]
        // public IActionResult GetInterestRates()
        // {
        //     return this.AlpsActionOk(_context.LoanSettings.SelectMany(p => p.InterestRates));
        // }
        // [HttpPost("publishnewrate")]
        // public async Task<IActionResult> PublishNewRate([FromBody] InterestRatePublishDto dto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     LoanSetting setting = DoGetLoanSetting();
        //     setting.PublishInterestRate(dto.StartExecutionDate, dto.Rate, User.Identity.Name);
        //     await _context.SaveChangesAsync();
        //     return this.AlpsActionOk(setting.InterestRates);
        // }
        [HttpPost("saveloansetting")]
        public async Task<IActionResult> SaveLoanSetting([FromBody] LoanSettingEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoanSetting setting = DoGetLoanSetting();
            if (setting.MinDepositDay != dto.MinDepositDay)
                setting.MinDepositDay = dto.MinDepositDay;
            if (setting.MinDepositAmount != dto.MinDepositAmount)
                setting.MinDepositAmount = dto.MinDepositAmount;
            var deletedRates = setting.InterestRates.Where(p => !dto.InterestRates.Any(l => l.ID == p.ID));
            var addedRates = dto.InterestRates.Where(p => !setting.InterestRates.Any(l => l.ID == p.ID));
            foreach (var r in deletedRates)
                setting.InterestRates.Remove(r);
            foreach (var r in addedRates)
                setting.InterestRates.Add(InterestRate.Create(r.StartExecutionDate, r.Rate, User.Identity.Name));
            await _context.SaveChangesAsync();
            return this.AlpsActionOk(setting);
        }
        [HttpGet("getloansetting")]
        public IActionResult GetLoanSetting()
        {
            var setting = DoGetLoanSetting();
            var dto = new LoanSettingEditDto
            {
                MinDepositAmount = setting.MinDepositAmount,
                MinDepositDay = setting.MinDepositDay,
                InterestRates = setting.InterestRates.Select(l => new InterestRateEditDto
                {
                    ID = l.ID,
                    Publisher = l.Publisher,
                    Rate = l.Rate,
                    StartExecutionDate = l.StartExecutionDate,
                    PublishDate = l.PublishDate

                }).ToList()
            };
            return this.AlpsActionOk(dto);
        }

        private LoanSetting DoGetLoanSetting()
        {

            LoanSetting setting = _context.LoanSettings.Include(p => p.InterestRates).FirstOrDefault();
            if (setting == null)
            {
                setting = LoanSetting.Create(0, 0);
                _context.LoanSettings.Add(setting);
            }
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
                v.Withdraw(dto.Date, dto.Amount, "", User.Identity.Name, GetInterestRates());
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

        [HttpPost("getinterestsummary")]
        public IActionResult GetSettlabeInterestSummary([FromBody]FilterDto filter)
        {
            var interestRates = GetInterestRates();
            List<LoanVoucher> vouchers;
            if (filter.Filter.ToLower() == "ysjs")
                vouchers = _context.LoanVouchers.Include(p => p.Lender).Where(p => !p.IsInvalid && p.Amount > 0).ToList();
            else
                vouchers = _context.LoanVouchers.Include(p => p.Lender).Where(p => !p.IsInvalid && p.Amount > 0 && p.Lender.Name.Contains(filter.Filter)).ToList();
            var dto = vouchers.Select(p => new
            {
                Lender = p.Lender.Name,
                Amount = p.Amount,
                Interest = p.CalculateQuarterInterest(interestRates)
            }).GroupBy(p => p.Lender).Select(p => new SettlableInterestSummaryDto
            {
                Lender = p.Key,
                TotalAmount = p.Sum(l => l.Amount),
                TotalInterest = p.Sum(l => l.Interest)
              ,
                Count = p.Count()
            });

            return this.AlpsActionOk(dto);

        }
        [HttpGet("getinteresetdetal/{hashCode}")]
        public IActionResult GetInteresetDetal([FromRoute]string hashCode)
        {

            var interestRates = GetInterestRates();
            var vouchers = _context.LoanVouchers.Include(p => p.Records).Where(p => p.Lender.Name == hashCode && p.Amount > 0).ToList();
            var detailDto = new SettleInterestDetailDto
            {
                Vouchers = vouchers.Select(l => new LoanVoucherListDto()
                {
                    ID = l.ID,
                    Date = l.DepositTime,
                    Amount = l.Amount,
                    Interest = l.CalculateQuarterInterest(interestRates),
                    Lender = hashCode,
                    InterestSettlable = l.CanSettleInterest()
                }),
                Records = vouchers.SelectMany(p => p.Records).Select(p => new LoanRecordDto
                {
                    IsInvalid = p.IsInvalid,
                    LoanVoucherID = p.LoanVoucherID,
                    ID = p.ID,
                    Date = p.OperateTime,
                    Name = hashCode,
                    Amount = p.Amount,
                    Interest = p.Interest,
                    Type = p.Type
                }).Where(p => p.Type == LoanRecordType.SettleInterest)
            };

            return this.AlpsActionOk(detailDto);
        }
        [HttpGet("getSettleInteresetPrintInfo/{id}")]
        public IActionResult GetSettleInteresetPrintInfo([FromRoute]string id)
        {
            var dto = new SettleInterestPrintDto { Lender = id, Interest = 0, Date = DateTimeOffset.Now };

            var list = _context.LoanVouchers.Where(p => p.Lender.Name == id).SelectMany(p => p.Records).Where(p => p.Type == LoanRecordType.SettleInterest &&
             p.OperateTime.Date == DateTimeOffset.Now.Date && !p.IsInvalid);
            dto.Amount = list.Sum(p => p.Amount);
            dto.Interest = list.Sum(p => p.Interest);
            dto.EndDate = LoanVoucher.GetSettlableDate().AddDays(-1);
            return this.AlpsActionOk(dto);
        }

    }


}