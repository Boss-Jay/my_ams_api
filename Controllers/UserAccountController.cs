using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("create-user")]
        public async Task<IActionResult> Create_User(userCreate_Dto _userCreate_Dto)
        {
            var username = _userCreate_Dto!.Firstname!.Substring(0, 1) +
                _userCreate_Dto!.Middlename!.Substring(0, 1) +
                _userCreate_Dto.Lastname;
            var user = new UserAccount
            {
                Firstname = _userCreate_Dto.Firstname,
                Lastname = _userCreate_Dto.Lastname,
                Suffix = _userCreate_Dto.Suffix,
                Branchid = _userCreate_Dto.Branchid,
                Date = _userCreate_Dto.Date,
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(_userCreate_Dto.Password),
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
    }
}
