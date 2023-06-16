using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Message
{
    public string Id { get; set; } = null!;

    public string ChatRoomId { get; set; } = null!;

    public string Message1 { get; set; } = null!;

    public string ImageSetId { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual ChatRoom ChatRoom { get; set; } = null!;

    public virtual ImageSet ImageSet { get; set; } = null!;
}
