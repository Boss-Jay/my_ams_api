using System.ComponentModel.DataAnnotations;

namespace Ams_Api.Dto
{
    public class userUpdate_Dto
    {
        [Required(ErrorMessage = "Firstname is required !")]
        public string? Firstname { get; set; }
        [Required(ErrorMessage = "Middlename is required !")]
        public string? Middlename { get; set; }
        [Required(ErrorMessage = "Lastname is requirede !")]
        public string? Lastname { get; set; }
        public string? Suffix { get; set; }
        [Required(ErrorMessage = "Branch is required !")]
        public int? Branchid { get; set; }
        [Required(ErrorMessage = "Role is required !")]
        public int? RoleId { get; set; }
        public DateTime? EditDate { get; set; } = DateTime.Now;

    }
}
