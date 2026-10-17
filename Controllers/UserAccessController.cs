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
        public async Task<IActionResult> Create_Access(
            [FromBody] accessUserAccess_Dto dto)
        {
            // Validate user
            var user = await _amsDbContext.UserAccounts
                .FindAsync(dto.UserId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found!"
                });
            }

            // Validate module lists
            if (dto.ModuleId == null ||
                dto.HasAccess == null ||
                dto.ModuleId.Count == 0 ||
                dto.ModuleId.Count != dto.HasAccess.Count)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid module access list."
                });
            }

            // Validate values before accessing the database
            if (dto.ModuleId.Any(id => id == null || id <= 0) ||
                dto.HasAccess.Any(access => access != 0 && access != 1) ||
                dto.ModuleId.Distinct().Count() != dto.ModuleId.Count)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid or duplicate module IDs, or invalid access values."
                });
            }

            for (int i = 0; i < dto.ModuleId.Count; i++)
            {
                int moduleId = dto.ModuleId[i]!.Value;
                int hasAccess = dto.HasAccess[i]!.Value;

                var module = await _amsDbContext.UserAccesses
                    .FindAsync(dto.UserId, moduleId);

                if (module == null)
                {
                    _amsDbContext.UserAccesses.Add(new UserAccess
                    {
                        UserId = dto.UserId,
                        ModuleId = moduleId,
                        Access = hasAccess
                    });
                }
                else
                {
                    module.Access = hasAccess;
                }
            }

            await _amsDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "User access saved successfully."
            });
        }
        [HttpGet("module-access-status")]
        public async Task<IActionResult> GetModuleAccessStatus(
            int userId, int moduleId)
        {
            var access = await _amsDbContext.UserAccesses
                .Where(x => x.UserId == userId &&
                            x.ModuleId == moduleId)
                .Select(x => (int?)x.Access)
                .FirstOrDefaultAsync();

            return Ok(access == 1 ? 1 : 0);
        }
    }
}
