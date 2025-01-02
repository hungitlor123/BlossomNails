using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ServiceImage
{
    public int ImageID { get; set; }

    public string? ImageName { get; set; }

    public string ImageURL { get; set; } = null!;

    public int? ServiceID { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Service? Service { get; set; }
}
