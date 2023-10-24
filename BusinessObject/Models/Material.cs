using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
