using Ams_Api.Data;
using Ams_Api.Dto;
using Ams_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace Ams_Api.Controllers
{
    [Route("api/supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly AmsDbContext _amsDbContext;
        public SupplierController(AmsDbContext amsDbContext)
        {
            _amsDbContext = amsDbContext;
        }
        [HttpGet("get-supptype")]
        public async Task<IActionResult> Get_Supplier_Type()
        {
            var type = await _amsDbContext.SupplierTypes
                .Select(r => new
                {
                    r.Id,
                    r.Description
                })
                .OrderBy(r => r.Description)
                .ToListAsync();
            return Ok(type);
        }
        [HttpGet("supplier-list")]
        public async Task<IActionResult> Supplier_List()
        {
            var supplier = await _amsDbContext.Suppliers
                .Select(s => new supplierList_Dto
                {
                    Type = s.Type,
                    Tin = s.Tin,
                    BranchCode = s.BranchCode,
                    Lastname = s.Lastname,
                    Firstname = s.Firstname,
                    MiddleInitial = s.MiddleInitial,
                    Suffix = s.Suffix,
                    CreatedDate = s.CreatedDate,
                    Corporation = s.Corporation,
                    Address = s.Address,
                    ZipCode = s.ZipCode,
                    Tradename = s.Tradename,
                    TaxReg = s.TaxReg
                })
                .ToListAsync();
            return Ok(supplier);
        }
        [HttpGet("check-tin-code")]
        public async Task<IActionResult> Check_TIN(
            [FromQuery] string tin ,
            [FromQuery] string branchcode)
        {
            if (string.IsNullOrWhiteSpace(tin))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "TIN is required."
                });
            }

            var exists = await _amsDbContext.Suppliers
                .AnyAsync(s => s.Tin == tin.Trim() && s.BranchCode == branchcode.Trim());

            return Ok(new
            {
                success = true,
                exists,
                message = exists
                    ? "TIN already exists."
                    : "TIN is available."
            });
        }
        [HttpPost("create-supplier")]
        public async Task<IActionResult> Create_Supplier( supplierCreate_Dto _Dto)
        {
            switch (_Dto.Type)
            {
                case 1: // Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Lastname) ||
                        string.IsNullOrWhiteSpace(_Dto.MiddleInitial) ||
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
            if (string.IsNullOrWhiteSpace(_Dto.Address))
                return BadRequest("The Address field must be filled in!");
     
            if (_Dto.ZipCode == 0)
                return BadRequest("The Zipcode field must be filled in!");

            var supplier = new Supplier
            {
                Type = _Dto.Type,
                Tin = _Dto.Tin,
                BranchCode = _Dto.BranchCode,
                Lastname = _Dto.Lastname,
                Firstname = _Dto.Firstname,
                MiddleInitial = _Dto.MiddleInitial,
                Suffix = _Dto.Suffix,
                Corporation = _Dto.Corporation,
                Address = _Dto.Address,
                ZipCode = _Dto.ZipCode,
                Tradename = _Dto.Tradename,
                Atcextension = (_Dto.Type == 1) ? "WI" : "WC",
                TaxReg = _Dto.TaxReg,
                Createdby = _Dto.Createdby
            };

            _amsDbContext.Suppliers.Add(supplier);
            await _amsDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Supplier created successfully."
            });
        }
        [HttpGet("supplier-list-by-Id")]
        public async Task<IActionResult> Supplier_List(int? Id)
        {
            var supplier = await _amsDbContext.Suppliers
                .Where(s => s.Id == Id)
                .Select(s => new supplierList_Dto
                {
                    Id = s.Id,
                    Type = s.Type,
                    Tin = s.Tin,
                    BranchCode = s.BranchCode,
                    Lastname = s.Lastname,
                    Firstname = s.Firstname,
                    MiddleInitial = s.MiddleInitial,
                    Suffix = s.Suffix,
                    CreatedDate = s.CreatedDate,
                    Corporation = s.Corporation,
                    Address = s.Address,
                    ZipCode = s.ZipCode,
                    Tradename = s.Tradename,
                    TaxReg = s.TaxReg
                })
                .FirstOrDefaultAsync();
            return Ok(supplier);
        }
        [HttpPut("update-supplier")]
        public async Task<IActionResult> Update_Supplier(int? Id, supplierUpdate_Dto _Dto)
        {
            var supplier = await _amsDbContext.Suppliers.FindAsync(Id);
            if (supplier == null)
                return BadRequest("Supplier is Not Found!");
            switch (_Dto.Type)
            {
                case 1: // Individual
                    if (string.IsNullOrWhiteSpace(_Dto.Lastname) ||
                        string.IsNullOrWhiteSpace(_Dto.MiddleInitial) ||
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
            if (string.IsNullOrWhiteSpace(_Dto.Address))
                return BadRequest("The Address field must be filled in!");

            if (_Dto.ZipCode == 0)
                return BadRequest("The Zipcode field must be filled in!");

            supplier!.Type = _Dto.Type;
            supplier.Tin = _Dto.Tin;
            supplier.BranchCode = _Dto.BranchCode;
            supplier.Lastname = _Dto.Lastname;
            supplier.Firstname = _Dto.Firstname;
            supplier.MiddleInitial = _Dto.MiddleInitial;
            supplier.Suffix = _Dto.Suffix;
            supplier.Corporation = _Dto.Corporation;
            supplier.Address = _Dto.Address;
            supplier.ZipCode = _Dto.ZipCode;
            supplier.Tradename = _Dto.Tradename;
            supplier.Atcextension = (_Dto.Type == 1) ? "WI" : "WC";
            supplier.TaxReg = _Dto.TaxReg;
            supplier.EditBy = _Dto.EditBy;
            supplier.EditDate = _Dto.EditDate;


            await _amsDbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Supplier update successfully."
            });
        }

        [HttpGet("get-searched-supplier")]
        public async Task<IActionResult> Get_Search_Supplier(
            string? SearchTIN, string ? SearchBranchCode)
        {
            var searchBranchCode = SearchBranchCode?.Trim().TrimStart('0');
            var supplierresult = await _amsDbContext.Suppliers
                 .Where(s =>
                     s.Tin == SearchTIN &&
                     (string.IsNullOrEmpty(SearchBranchCode) ||
                      s.BranchCode!.TrimStart('0') == searchBranchCode))
                 .ToListAsync();
            return Ok(supplierresult);
        }
    }
}
