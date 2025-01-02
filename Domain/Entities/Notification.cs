using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Notification
{
    public int NotificationID { get; set; }

    public Guid? Receiver { get; set; }

    public string Type { get; set; } = null!;

    public int? ObjectID { get; set; }

    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual User? ReceiverNavigation { get; set; }
}
