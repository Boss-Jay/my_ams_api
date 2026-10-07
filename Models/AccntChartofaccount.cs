using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class AccntChartofaccount
{
    public int Id { get; set; }

    public int? CoaId { get; set; }

    public string? Description { get; set; }

    public int? TypeId { get; set; }

    public int? TagId { get; set; }

    public int? Level { get; set; }

    public int? SubCoaId { get; set; }

    public DateTime? Date { get; set; }

    public int? AddedBy { get; set; }

    public int? Editby { get; set; }

    public DateTime? EditDate { get; set; }

    public int? BalanceType { get; set; }

    public int? Status { get; set; }
}
