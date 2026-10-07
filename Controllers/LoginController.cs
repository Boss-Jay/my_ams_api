using Ams_Api.Data;
using Ams_Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ams_Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AmsDbContext _amscontext;
        private readonly IConfiguration _configuration;

        public LoginController(AmsDbContext amsDbContext,
            IConfiguration configuration)
        {
            _amscontext = amsDbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(userLogin_Dto userLogin_Dto)
        {
            var user = await _amscontext.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == userLogin_Dto.Username);
            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }
            if (user.Status == 0)
            {
                return Unauthorized(new
                {
                    message = "Your Account is not Active. Please contact Administrator."
                });
            }
            if (!BCrypt.Net.BCrypt.Verify(userLogin_Dto.Password,user.Password))
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(ClaimTypes.Name,
                    user.Username!
                ),

                new Claim(ClaimTypes.Role,
                    user.RoleId.ToString()!
                ),

                new Claim("BranchId",user.Branchid.ToString()!
                )
            };
            // JWT key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["JwtConfig:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            var tokenValidityMins =
            int.Parse(_configuration["JwtConfig:TokenValidityMins"]!);
            // Create token
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenValidityMins),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler()
                .WriteToken(token);
            var tokenValidity = DateTime.UtcNow.AddMinutes(tokenValidityMins);
            return Ok(new
            {

                userid = user.Id,
                lastname = user.Lastname,
                firstname = user.Firstname,
                middlename = user.Middlename,
                branchid = user.Branchid,
                ismanager = 1,
                roleid = user.RoleId,
                lastbranchidaccess = user.LastBranchIdAccess,
                token = jwtToken,
                tokenValidity = tokenValidityMins
            });
        }
    }
}
