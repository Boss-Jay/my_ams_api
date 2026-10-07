using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class TenantsVatoutput
{
    public int Id { get; set; }

    public int? TenantsId { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? ExemptAmount { get; set; }

    public decimal? TaxableAmount { get; set; }

    public decimal? VatInput { get; set; }

    public DateTime? Date { get; set; }

    public int? Addedby { get; set; }

    public int? Editby { get; set; }

    public DateTime? EditDate { get; set; }
}
