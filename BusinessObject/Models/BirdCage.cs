using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BirdCage
{
    public int CageId { get; set; }

    public string CageName { get; set; } = null!;

    public string? ImagePath { get; set; }

    public string BirdType { get; set; } = null!;

    public double Size { get; set; }

    public int NumOfPerches { get; set; }

    public string Accessories { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int? CreatedByCustomer { get; set; }

    public bool IsAvailable { get; set; }

    public virtual User? CreatedByCustomerNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
