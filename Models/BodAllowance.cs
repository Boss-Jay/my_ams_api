using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class BodAllowance
{
    public int Id { get; set; }

    public int? OfficerId { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Amount { get; set; }
}
