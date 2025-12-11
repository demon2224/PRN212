using System;
using System.Collections.Generic;

namespace WPF_Tu_Hoc.Entities;

public partial class Sach
{
    public string Id { get; set; } = null!;

    public string? TenSach { get; set; }

    public string? GiaBan { get; set; }

    public int? NamXuatBan { get; set; }
}
