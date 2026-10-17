using System.ComponentModel.DataAnnotations;

namespace Ams_Api.Dto
{
    public class branchUpdate_Dto
    {
        [Required(ErrorMessage = "Branch Name is Required !")]
        public int? CompanyId { get; set; }
        [Required(ErrorMessage = "Branch Name is Required !")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Address is Required !")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Zipcode is Required !")]
        public int? Zipcode { get; set; }
        public int? ManagerId { get; set; }
        [Required(ErrorMessage = "Contact Number is Required !")]
        public string? Contact1 { get; set; }
        public string? Contact2 { get; set; }
        public string? Tin { get; set; }
        public string? Code { get; set; }
        public string? Ocn { get; set; }
        [Required(ErrorMessage = "Email Name is Required !")]
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? Accountant { get; set; }
        public string? Rdo { get; set; }
    }
}
