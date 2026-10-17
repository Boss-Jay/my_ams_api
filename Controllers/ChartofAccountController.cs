using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Controllers
{
    [Route("api/coa")]
    [ApiController]
    public class ChartofAccountController : ControllerBase
    {
        private readonly AmsDbContext _amsDbContext;
        public ChartofAccountController(AmsDbContext amsDbContext)
        {
            _amsDbContext = amsDbContext;
        }

        [HttpGet("get-coa-by-id")]
        public async Task<IActionResult> Get_Coa(int coaid)
        {
            var coa = await _amsDbContext.AccntChartofaccounts
                .Where(c => c.CoaId == coaid)
                .Select(c => new coaList_Dto
                {
                    CoaId = c.CoaId,
                    CoaDesc = c.CoaDesc,
                    Type = c.Type,
                    Tag = c.Tag,
                    Level = c.Level,
                    SubCoaId = c.SubCoaId,
                    AddedDate = c.AddedDate,
                    AddedBy = c.AddedBy,
                    EditBy = c.EditBy,
                    EditDate = c.EditDate,
                    BalType = c.BalType,
                    Active = c.Active

                }).ToListAsync();
            return Ok(coa);
        }
      
       
        [HttpGet("coa-tree")]
        public async Task<IActionResult> Coa_Tree()
        {
            var accounts = await _amsDbContext.AccntChartofaccounts
                .AsNoTracking()
                .OrderBy(a => a.CoaId)
                .Select(a => new
                {
                    a.CoaId,
                    a.CoaDesc,
                    a.Type,
                    a.Tag,
                    a.Level,
                    a.SubCoaId
                })
                .ToListAsync();

            var lookup = accounts.ToDictionary(
                a => a.CoaId,
                a => new coaTree_Dto
                {
                    CoaId = a.CoaId,
                    CoaDesc = a.CoaDesc,
                    Type = a.Type,
                    Tag = a.Tag,
                    Level = a.Level
                    
                });

            foreach (var account in accounts)
            {
                if (account.SubCoaId.HasValue &&
                    account.SubCoaId.Value != account.CoaId &&
                    lookup.TryGetValue(account.SubCoaId.Value, out var parent))
                {
                    parent.Children.Add(lookup[account.CoaId]);
                }
            }

            var tree = accounts
                .Where(a =>
                    !a.SubCoaId.HasValue ||
                    a.SubCoaId.Value == a.CoaId ||
                    !lookup.ContainsKey(a.SubCoaId.Value))
                .Select(a => lookup[a.CoaId])
                .ToList();

            return Ok(tree);
        }
        [HttpGet("get-main-account")]
        public async Task<IActionResult> Get_Main_Account(int? TypeId)
        {
            var main = await _amsDbContext.AccntChartofaccounts
                .Where(a => a.Type == TypeId)
                .Select(a => new
                {
                    a.CoaId,
                    a.CoaDesc
                })
                .FirstOrDefaultAsync();
            return Ok(main);
        }
        [HttpGet("get-max-accnt-code")]
        public async Task<IActionResult> Get_Max_Code(int coaid,int level)
        {
            var count = await _amsDbContext.AccntChartofaccounts
                .Where(a => a.SubCoaId == coaid)
                .MaxAsync(a =>(int?)a.CoaId);
            int nextcode = 0;
            if (count == null)
                count = coaid;
            if (level == 1 )
            {
                nextcode = (count ?? coaid) + 1000;
            }
            else
            {
                nextcode = (int)(count + 1)!;
            }
            

            return Ok(new
            {
                success = true,
                maxaccount = nextcode
            });
        }
        [HttpPost("create-account")]
        public async Task<IActionResult> Save_Account(coaCreate_Dto _Dto)
        {
            if (string.IsNullOrWhiteSpace(_Dto.CoaDesc))
                return BadRequest("Account Description must be filled in!");

            var newaccount = new AccntChartofaccount
            {
                CoaId = _Dto.CoaId,
                CoaDesc = _Dto.CoaDesc,
                Type = _Dto.Type,
                Tag = _Dto.Tag,
                Level = _Dto.Level,
                SubCoaId = _Dto.SubCoaId,
                AddedDate = _Dto.AddedDate,
                AddedBy = _Dto.AddedBy,
                BalType = _Dto.BalType,
                Active = _Dto.Active
            };
            _amsDbContext.AccntChartofaccounts.Add(newaccount);
            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Account Title is successfully added"
            });
        }
    }
}
