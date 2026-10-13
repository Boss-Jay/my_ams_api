using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Controllers
{
    [Route("api/useraccess")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
        private readonly AmsDbContext _amsDbContext;
        public UserAccessController(AmsDbContext amsDbContext)
        {
            _amsDbContext = amsDbContext;
        }
        [HttpGet("module-list")]
        public async Task<IActionResult> Module_list()
        {
            var module = await _amsDbContext.UserModules
                .Select(m => new accessModule_Dto
                {
                    Id = m.Id,
                    Module = m.Module
                }).ToListAsync();
            return Ok(module);
        }
        [HttpPost("create-access")]
        public async Task<IActionResult> Create_Access(accessUserAccess_Dto accessUserAccess_Dto)
        {
            var user = await _amsDbContext.UserAccounts.FindAsync(accessUserAccess_Dto.UserId);
            if (user == null)
                return NotFound("User is Not Found !");
            var module =await _amsDbContext.UserAccesses
                .FindAsync(accessUserAccess_Dto.UserId, accessUserAccess_Dto.ModuleId);
            if (module == null)
            {
                var useraccess = new UserAccess
                {
                    UserId = accessUserAccess_Dto.UserId,
                    ModuleId = accessUserAccess_Dto.ModuleId,
                    Access = accessUserAccess_Dto.AccessId
                };
                _amsDbContext.UserAccesses.Add(useraccess);
            }
            else
            {
                module.Access = accessUserAccess_Dto.AccessId;
           
            }
            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "User access added successfully."
            });
        }
    }
}
