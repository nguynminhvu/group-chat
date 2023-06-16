using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class ChatRoom
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Creator { get; set; } = null!;

    public string Admin { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Secret { get; set; }

    public bool IsPrivate { get; set; }

    public virtual ICollection<ChatRoomUserMapping> ChatRoomUserMappings { get; set; } = new List<ChatRoomUserMapping>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
