using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public int? Type { get; set; }

    public string? Tin { get; set; }

    public string? BranchCode { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? MiddleInitial { get; set; }

    public string? Suffix { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Corporation { get; set; }

    public string? Address { get; set; }

    public int? ZipCode { get; set; }

    public string? Tradename { get; set; }

    public string? Atcextension { get; set; }

    public int? TaxReg { get; set; }

    public int? Createdby { get; set; }

    public DateTime? EditDate { get; set; }

    public int? EditBy { get; set; }
}
