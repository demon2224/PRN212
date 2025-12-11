using System;
using System.Collections.Generic;

namespace PE_CE180905_LeAnhTuan.Models;

public partial class BookStore
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }
}
