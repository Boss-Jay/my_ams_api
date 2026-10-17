using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Controllers
{
    [Route("api/useraccnt")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly AmsDbContext _amscontext;

        public UserAccountController(AmsDbContext amsDbContext)
        {
            _amscontext = amsDbContext;
            
        }
        [HttpGet("user-list")]
        public async Task<IActionResult> User_List()
        {
            var user = await _amscontext.UserAccounts
                .Select(u => new userList_Dto
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Middlename = u.Middlename,
                    Lastname = u.Lastname,
                    Suffix = u.Suffix,
                    Branchid = u.Branchid,
                    BranchDesc = _amscontext.Branches
                    .Where(b => b.Branchid == u.Branchid)
                    .Select(b => b.Name)
                    .FirstOrDefault() ?? "",
                    Date = u.Date,
                    Username = u.Username,
                    RoleId = u.RoleId,
                    RoleDesc = _amscontext.UserRoles
                    .Where(b => b.Id == u.RoleId)
                    .Select(b => b.Description)
                    .FirstOrDefault() ?? "",
                    Status = u.Status,
                    StatusDesc = (u.Status == 1) ? "Active" : "Non-Active"
                })
                .OrderByDescending(u => u.Date)
                .ToListAsync();
            return Ok(user);
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> Create_User(userCreate_Dto _userCreate_Dto)
        {
            var username =  _amscontext.UserAccounts
                .Where(u => u.Username == _userCreate_Dto.Username)
                .Select(u => u.Username)
                .FirstOrDefault();
           if(username != null)
            {
                return BadRequest("Username is already exist !");
            }
            
            var user = new UserAccount
            {
                Firstname = _userCreate_Dto.Firstname,
                Lastname = _userCreate_Dto.Lastname,
                Middlename = _userCreate_Dto.Middlename,
                Suffix = _userCreate_Dto.Suffix,
                Branchid = _userCreate_Dto.Branchid,
                Date = _userCreate_Dto.Date,
                Username = _userCreate_Dto.Username,
                RoleId = _userCreate_Dto.RoleId,
                Status = 1
            };
            _amscontext.UserAccounts.Add(user);
            await _amscontext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "User created successfully."
            });
        }
        [HttpGet("get-roles-list")]
        public async Task<IActionResult> Get_Role_List()
        {
            var role = await _amscontext.UserRoles
                .Select(r => new
                {
                    r.Id,
                    r.Description
                })
                .OrderBy(r => r.Description)
                .ToListAsync();
            return Ok(role);
        }
        [HttpGet("get-user-listing")]
        public async Task<IActionResult> Get_User_List()
        {
            var users = await _amscontext.UserAccounts
                .OrderBy(r => r.Lastname)
                .Select(r => new
                {
                    r.Id,
                    Fullname = r.Lastname + ", " +
                               r.Firstname + " " +
                               (r.Middlename != null && r.Middlename.Length > 0
                                   ? r.Middlename.Substring(0, 1) + "."
                                   : "")
                })
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("get-branch-list")]
        public async Task<IActionResult> Get_Branch_List()
        {
            var branch = await _amscontext.Branches
                .Select(b => new
                {
                    b.Branchid,
                    b.Name
                })
                .OrderBy(b => b.Name)
                .ToListAsync();
            return Ok(branch);
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> Get_User(int? Id)
        {
            var user = await _amscontext.UserAccounts
                .Where(u => u.Id == Id)
                .Select(u => new userList_Dto
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Middlename = u.Middlename,
                    Lastname = u.Lastname,
                    Suffix = u.Suffix,
                    Branchid = u.Branchid,
                    BranchDesc = _amscontext.Branches
                    .Where(b => b.Branchid == u.Branchid)
                    .Select(b => b.Name)
                    .FirstOrDefault() ?? "",
                    Date = u.Date,
                    Username = u.Username,
                    RoleId = u.RoleId,
                    RoleDesc = _amscontext.UserRoles
                    .Where(b => b.Id == u.RoleId)
                    .Select(b => b.Description)
                    .FirstOrDefault() ?? "",
                    Status = u.Status,
                    StatusDesc = (u.Status == 1) ? "Active" : "Non-Active"
                }).FirstOrDefaultAsync();
            return Ok(user);
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> Update_User(int? Id, userUpdate_Dto userUpdate_Dto )
        {
            var user = await _amscontext.UserAccounts.FindAsync(Id);

            if(user == null)
            {
                return BadRequest("User is not found!");
            }
            user.Firstname = userUpdate_Dto.Firstname;
            user.Middlename = userUpdate_Dto.Middlename;
            user.Lastname = userUpdate_Dto.Lastname;
            user.Suffix = userUpdate_Dto.Suffix;
            user.Branchid = userUpdate_Dto.Branchid;
            user.RoleId = userUpdate_Dto.RoleId;
            user.EditDate = userUpdate_Dto.EditDate;

            await _amscontext.SaveChangesAsync();
            return Ok(new
            {
                message = "User is successfully updated !"
            });
        }
        [HttpPut("update-user-status")]
        public async Task<IActionResult> Update_User_Status(int? Id, int? StatusId)
        {
            var user = await _amscontext.UserAccounts.FindAsync(Id);

            if (user == null)
            {
                return BadRequest("User is not found!");
            }
            user.Status = StatusId;
            await _amscontext.SaveChangesAsync();
            return Ok(new
            {
                message = StatusId == 1
                  ? "User has been activated."
                  : "User has been deactivated."
            });
        }
        [HttpGet("dashboard_count")]
        public async Task<IActionResult> Dashboard_Count()
        {
            var query = _amscontext.UserAccounts.AsQueryable();
            var count = new userDashboard_Dto { 
                TotalUser = await query.CountAsync(),
                User_Active = await query.CountAsync(u => u.Status == 1),
                User_Not_Active = await query.CountAsync(u => u.Status == 0)
            };
            return Ok(count);
        }
        [HttpPut("user-change-pass")]
        public async Task<IActionResult> Change_Password(userChange_Password _userChange_Password)
        {
            var user = await  _amscontext.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == _userChange_Password.Username);
            if(user == null)
                return NotFound("User is not found!");
            if(_userChange_Password.NewPassword != _userChange_Password.RetypeNewPassword)
                return BadRequest("Password is not match!");

            user.Password = BCrypt.Net.BCrypt.HashPassword(_userChange_Password.RetypeNewPassword);
            await _amscontext.SaveChangesAsync();

            return Ok(new
            {
                message = "Password changed successfully."
            });
        }
    }
}
