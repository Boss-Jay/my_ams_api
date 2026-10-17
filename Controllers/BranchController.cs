using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

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
                    CompanyName = _amsDbContext.Companies
                    .Where(c => c.Id == b.CompanyId)
                    .Select(c => c.Accronym)
                    .FirstOrDefault(),
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
        [HttpGet("get-company-list")]
        public async Task<IActionResult> Get_Company_Listing()
        {
            var company = await _amsDbContext.Companies
                .Select(r => new
                {
                    r.Id,
                    r.Description
                })
                .OrderBy(r => r.Description)
                .ToListAsync();
            return Ok(company);
        }
        [HttpPost("create-branch")]
        public async Task<IActionResult> Create_Branch(branchCreate_Dto _branchCreate_Dto)
        {
            var branch = _amsDbContext.Branches
                .Where(b => b.Name == _branchCreate_Dto.Name).FirstOrDefault();
            if (branch != null)
                return BadRequest("Branch Name is already exist!");
            var newbranch = new Branch
            {
                CompanyId = _branchCreate_Dto.CompanyId,
                Name = _branchCreate_Dto.Name,
                Address = _branchCreate_Dto.Address,
                Zipcode = _branchCreate_Dto.Zipcode,
                ManagerId = _branchCreate_Dto.ManagerId,
                Contact1 = _branchCreate_Dto.Contact1,
                Contact2 = _branchCreate_Dto.Contact2,
                Tin = _branchCreate_Dto.Tin,
                Code = _branchCreate_Dto.Code,
                Ocn = _branchCreate_Dto.Ocn,
                Email1 = _branchCreate_Dto.Email1,
                Email2 = _branchCreate_Dto.Email2,
                Accountant = _branchCreate_Dto.Accountant,
                Rdo = _branchCreate_Dto.Rdo
            };
            _amsDbContext.Branches.Add(newbranch);
            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Branch created successfully."
            });
        }
        [HttpGet("get-branch")]
        public async Task<IActionResult> Get_Branch(int? Branchid)
        {
            var branch = await _amsDbContext.Branches
            .Where(b => b.Branchid == Branchid)
             .Select(b => new branchList_Dto
             {
                 Branchid = b.Branchid,
                 CompanyId = b.CompanyId,
                 CompanyName = _amsDbContext.Companies
                 .Where(c => c.Id == b.CompanyId)
                 .Select(c => c.Accronym)
                 .FirstOrDefault(),
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
             }).FirstOrDefaultAsync();
            return Ok(branch);
        }
        [HttpPut("update-branch")]
        public async Task<IActionResult> Update_Branch(int? branchid, branchUpdate_Dto _branchUpdate_Dto)
        {
            var branch = await _amsDbContext.Branches.FindAsync(branchid);
            if (branch == null)
            {
                return BadRequest("Branch is Not Found!");
            }

            /*var branchname = _amsDbContext.Branches
                 .Where(b => b.Name == _branchUpdate_Dto.Name).FirstOrDefault();
            if (branch != null)
                return BadRequest("Branch Name is already exist!");*/

            branch!.CompanyId = _branchUpdate_Dto.CompanyId;
            branch.Name = _branchUpdate_Dto.Name;
            branch.Address = _branchUpdate_Dto.Address;
            branch.Zipcode = _branchUpdate_Dto.Zipcode;
            branch.ManagerId = _branchUpdate_Dto.ManagerId;
            branch.Contact1 = _branchUpdate_Dto.Contact1;
            branch.Contact2 = _branchUpdate_Dto.Contact2;
            branch.Tin = _branchUpdate_Dto.Tin;
            branch.Code = _branchUpdate_Dto.Code;
            branch.Ocn = _branchUpdate_Dto.Ocn;
            branch.Email1 = _branchUpdate_Dto.Email1;
            branch.Email2 = _branchUpdate_Dto.Email2;
            branch.Accountant = _branchUpdate_Dto.Accountant;
            branch.Rdo = _branchUpdate_Dto.Rdo;
            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                message = "Branch is successfully updated !"
            });
        }
        [HttpGet("branch-list-sidebar")]
        public async Task<IActionResult> Get_Branch_List_Sidebar()
        {
            var branch = await _amsDbContext.Branches
                .Select(b => new branchListSidebar_Dto
                {
                   BranchId = b.Branchid,
                   Name = b.Name,
                   Address = b.Address
                })
                .OrderBy(b => b.BranchId )
                .ToListAsync();
            return Ok(branch);
        }
        [HttpPost("update-last-branch")]
        public async Task<IActionResult> Update_Last_Branch(
            [FromBody] branchUpdateLastUserBranch_Dto dto)
        {
            var user = await _amsDbContext.UserAccounts
                .FindAsync(dto.UserId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found."
                });
            }

            // TODO: Verify the user is authorized to access this branch.

            user.LastBranchIdAccess = dto.BranchId;

            await _amsDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Last selected branch updated successfully."
            });
        }
    }
}
