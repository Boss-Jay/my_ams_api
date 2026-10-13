using System;
using System.Collections.Generic;

namespace Ams_Api.Models;

public partial class UserAccess
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ModuleId { get; set; }

    public int? Access { get; set; }
}
