using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ChatRoomUserMapping> ChatRoomUserMappings { get; set; } = new List<ChatRoomUserMapping>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
