

namespace Data.Entities;

public partial class Image
{
    public string Id { get; set; } = null!;

    public string ImageSetId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ImageSet ImageSet { get; set; } = null!;
}
