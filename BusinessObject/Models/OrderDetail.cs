using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int? CageId { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }

    public virtual BirdCage? Cage { get; set; }

    public virtual Order Order { get; set; } = null!;
}
