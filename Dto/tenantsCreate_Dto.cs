using System.ComponentModel.DataAnnotations;

namespace Ams_Api.Dto
{
    public class tenantsCreate_Dto
    {
  
        public int? Type { get; set; }
        public string? Corporation { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
   
        public string? Tin { get; set; }

        public string? TinCode { get; set; }
        public string? AddressDetails { get; set; }
 
        public string? City { get; set; }
        public int? Zipcode { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public int? Status { get; set; } = 1;
        public int? Addedby { get; set; }
    }
}
