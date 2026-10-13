using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class BirAtc
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public decimal? Rate { get; set; }
}
