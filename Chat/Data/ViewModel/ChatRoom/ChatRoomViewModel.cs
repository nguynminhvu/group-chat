namespace Data.ViewModel.ChatRoom
{
    public class ChatRoomViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string Admin { get; set; } = null!;

        public string? Secret { get; set; }

        public bool IsPrivate { get; set; }
    }
}
