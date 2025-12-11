using System;
using System.Collections.Generic;

namespace WPF_Tu_Hoc.Entities;

public partial class KhachHang
{
    public int Id { get; set; }

    public string? HoVaTen { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? DiaChi { get; set; }
}
