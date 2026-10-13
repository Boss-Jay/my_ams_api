using Ams_Api.Data;
using Ams_Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Controllers
{
    [Route("api/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly AmsDbContext _amsDbContext;
        public BranchController(AmsDbContext amsDbContext)
        {
            _amsDbContext = amsDbContext;
        }
        [HttpGet("branch-list")]
        public async Task<IActionResult> Branch_List()
        {
            var branch = await _amsDbContext.Branches
                .Select(b => new branchList_Dto
                {
                    Branchid = b.Branchid,
                    CompanyId = b.CompanyId,
                    Name = b.Name,
                    Address = b.Address,
                    Zipcode = b.Zipcode,
                    ManagerId = b.ManagerId,
                    ManagerName = _amsDbContext.UserAccounts
                    .Where(u => u.Id == b.ManagerId)
                    .Select(u => u.Lastname + ", " + u.Firstname + " " + u.Middlename)
                    .FirstOrDefault(),
                    Contact1 = b.Contact1,
                    Contact2 = b.Contact2,
                    Tin = b.Tin,
                    Code = b.Code,
                    Ocn = b.Ocn,
                    Date = b.Date,
                    Email1 = b.Email1,
                    Email2 = b.Email2,
                    Accountant = b.Accountant,
                    AccountantName = _amsDbContext.UserAccounts
                    .Where(u => u.Id == b.Accountant)
                    .Select(u => u.Lastname + ", " + u.Firstname + " " + u.Middlename)
                    .FirstOrDefault(),
                    Rdo = b.Rdo
                }).ToListAsync();
            return Ok(branch);
        }
    }
}
