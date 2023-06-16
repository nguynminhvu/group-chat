using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string ImageSetId { get; set; } = null!;

    public string AccId { get; set; } = null!;

    public virtual Account Acc { get; set; } = null!;

    public virtual ImageSet ImageSet { get; set; } = null!;
}
