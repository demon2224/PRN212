using System;
using System.Collections.Generic;

namespace AnhTuanPEChuan.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public int? Role { get; set; }
}
