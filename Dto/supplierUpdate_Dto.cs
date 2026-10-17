using System.ComponentModel.DataAnnotations;

namespace Ams_Api.Dto
{
    public class supplierUpdate_Dto
    {
        [Required(ErrorMessage = "Supplier Type is required!")]
        public int? Type { get; set; }
        [Required(ErrorMessage = "Supplier TIN is required!")]
        public string? Tin { get; set; }
        [Required(ErrorMessage = "Supplier TIN Branch Code is required!")]
        public string? BranchCode { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? MiddleInitial { get; set; }
        public string? Suffix { get; set; }
        public string? Corporation { get; set; }
        [Required(ErrorMessage = "Supplier Address is required!")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Supplier Zipcode is required!")]
        public int? ZipCode { get; set; }
        public string? Tradename { get; set; }
        [Required(ErrorMessage = "Supplier Tax Registered is required!")]
        public int? TaxReg { get; set; }
        public DateTime? EditDate { get; set; } = DateTime.Now;
        public int? EditBy { get; set; }
    }
}
