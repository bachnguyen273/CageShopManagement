using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string ReceiverName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Status Status { get; set; } = null!;
}
