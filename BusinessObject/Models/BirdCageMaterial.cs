using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BirdCageMaterial
{
    public int? CageId { get; set; }

    public int? MaterialId { get; set; }

    public int Quantity { get; set; }

    public virtual BirdCage? Cage { get; set; }

    public virtual Material? Material { get; set; }
}
