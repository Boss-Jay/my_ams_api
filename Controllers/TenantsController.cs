using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ams_Api.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly AmsDbContext _amsDbContext;
        public TenantsController(AmsDbContext amsDbContext)
        {
            _amsDbContext = amsDbContext;
        }

        [HttpGet("tenants-list")]
        public async Task<IActionResult> Tenants_List()
        {
            var tenants = await _amsDbContext.Tenants
                .Select(t => new tenantsList_Dto
                {
                    Id = t.Id,
                    Type = t.Type,
                    Corporation = t.Corporation,
                    Lastname = t.Lastname,
                    Firstname = t.Firstname,
                    Middlename  = t.Middlename,
                    Tin = t.Tin,
                    TinCode = t.TinCode,
                    AddressDetails = t.AddressDetails,
                    City = t.City,
                    Zipcode = t.Zipcode,
                    Date = t.Date,
                    Status = t.Status
                })
                .ToListAsync();
            return Ok(tenants);
        }
        [HttpGet("tenants-list-by-Id")]
        public async Task<IActionResult> Tenants_List(int Id)
        {
            var tenants = await _amsDbContext.Tenants
                .Where(t => t.Id == Id)
                .Select(t => new tenantsList_Dto
                {
                    Id = t.Id,
                    Type = t.Type,
                    Corporation = t.Corporation,
                    Lastname = t.Lastname,
                    Firstname = t.Firstname,
                    Middlename = t.Middlename,
                    Tin = t.Tin,
                    TinCode = t.TinCode,
                    AddressDetails = t.AddressDetails,
                    City = t.City,
                    Zipcode = t.Zipcode,
                    Date = t.Date,
                    Status = t.Status
                })
                .FirstOrDefaultAsync();
            return Ok(tenants);
        }
        [HttpPost("create-tenant")]
        public async  Task<IActionResult> Create_Tenants(tenantsCreate_Dto _Dto)
        {
         
            switch (_Dto.Type)
            {
                case 1: // Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Lastname) ||
                        string.IsNullOrWhiteSpace(_Dto.Middlename) ||
                        string.IsNullOrWhiteSpace(_Dto.Firstname))
                    {
                        return BadRequest("The Individual fields must be filled in!");
                    }
                    break;
                case 2: // Not Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Corporation))
                        return BadRequest("The Corporation Name must be filled in!");
                    break;
            }
            if (string.IsNullOrWhiteSpace(_Dto.AddressDetails))
                return BadRequest("The Address field must be filled in!");
            if (string.IsNullOrWhiteSpace(_Dto.City))
                return BadRequest("The City field must be filled in!");
            if (_Dto.Zipcode == 0)
                return BadRequest("The Zipcode field must be filled in!");


            var newtenant = new Tenant
            {
                Type = _Dto.Type,
                Corporation = _Dto.Corporation,
                Lastname = _Dto.Lastname,
                Firstname = _Dto.Firstname,
                Middlename = _Dto.Middlename,
                Tin = _Dto.Tin,
                TinCode = _Dto.TinCode,
                AddressDetails = _Dto.AddressDetails,
                City = _Dto.City,
                Zipcode = _Dto.Zipcode,
                Date = _Dto.Date,
                Status = _Dto.Status,
                Addedby = _Dto.Addedby
            };
            _amsDbContext.Tenants.Add(newtenant);
            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Tenants is created successfully. "
            });
        }
        [HttpPut("update-tenant")]
        public async Task<IActionResult> Update_Tenants(tenantsUpdate_Dto _Dto)
        {
            var tenants = await _amsDbContext.Tenants.FindAsync(_Dto.Id);
            if(tenants == null)
                return BadRequest("Supplier is Not Found!");
            switch (_Dto.Type)
            {
                case 1: // Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Lastname) ||
                        string.IsNullOrWhiteSpace(_Dto.Middlename) ||
                        string.IsNullOrWhiteSpace(_Dto.Firstname))
                    {
                        return BadRequest("The Individual fields must be filled in!");
                    }
                    break;
                case 2: // Not Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Corporation))
                        return BadRequest("The Corporation Name must be filled in!");
                    break;
            }
            if (string.IsNullOrWhiteSpace(_Dto.AddressDetails))
                return BadRequest("The Address field must be filled in!");
            if (string.IsNullOrWhiteSpace(_Dto.City))
                return BadRequest("The City field must be filled in!");
            if (_Dto.Zipcode == 0)
                return BadRequest("The Zipcode field must be filled in!");

            tenants!.Type = _Dto.Type;
            tenants.Tin = _Dto.Tin;
            tenants.TinCode = _Dto.TinCode;
            tenants.Lastname = _Dto.Lastname;
            tenants.Firstname = _Dto.Firstname;
            tenants.Middlename  = _Dto.Middlename;
            tenants.Corporation = _Dto.Corporation;
            tenants.AddressDetails = _Dto.AddressDetails;
            tenants.City = _Dto.City;
            tenants.Zipcode = _Dto.Zipcode;

            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Tenants is created successfully. "
            });
        }
        [HttpGet("get-searched-tenants")]
        public async Task<IActionResult> Get_Search_Tenants(
       string? SearchTIN, string? SearchBranchCode)
        {
            var searchBranchCode = SearchBranchCode?.Trim().TrimStart('0');
            var supplierresult = await _amsDbContext.Tenants
                 .Where(s =>
                     s.Tin == SearchTIN &&
                     (string.IsNullOrEmpty(SearchBranchCode) ||
                      s.TinCode!.TrimStart('0') == searchBranchCode))
                 .ToListAsync();
            return Ok(supplierresult);
        }
    }
}
