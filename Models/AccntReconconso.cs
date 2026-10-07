using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class AccntReconconso
{
    public int Id { get; set; }

    public int? BranchId { get; set; }

    public string? BranchDesc { get; set; }

    public DateTime? Date { get; set; }

    public int? Tin { get; set; }

    public string? Atc { get; set; }

    public decimal? Exempt { get; set; }

    public decimal? ZeroRated { get; set; }

    public decimal? Services { get; set; }

    public decimal? CapsGoods { get; set; }

    public decimal? OtherCapsGoods { get; set; }

    public decimal? Vat { get; set; }

    public decimal? WithTax { get; set; }

    public string? BranchCode { get; set; }

    public int? Sequence { get; set; }

    public decimal? WithTaxBase { get; set; }

    public int? AttachmentId { get; set; }
}
