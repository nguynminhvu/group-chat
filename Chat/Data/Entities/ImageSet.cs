using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class ImageSet
{
    public string Id { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
