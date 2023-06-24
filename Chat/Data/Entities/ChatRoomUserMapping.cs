namespace Data.Entities;

public partial class ChatRoomUserMapping
{
    public string Id { get; set; } = null!;

    public string AccId { get; set; } = null!;

    public string ChatRoomId { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual Account Acc { get; set; } = null!;

    public virtual ChatRoom ChatRoom { get; set; } = null!;
}
