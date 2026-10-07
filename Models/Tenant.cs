using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class Tenant
{
    public int Id { get; set; }

    public int? Type { get; set; }

    public string? Corporation { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Tin { get; set; }

    public string? TinCode { get; set; }

    public string? AddressDetails { get; set; }

    public string? City { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public int? Addedby { get; set; }
}
