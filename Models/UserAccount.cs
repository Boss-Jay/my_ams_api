using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public string? CompanyId { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Lastname { get; set; }

    public string? Suffix { get; set; }

    public int? Branchid { get; set; }

    public DateTime? Date { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public DateTime? EditDate { get; set; }

    public int? Status { get; set; }

    public int? LastBranchIdAccess { get; set; }
}
