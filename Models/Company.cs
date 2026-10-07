using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Accronym { get; set; }
}
