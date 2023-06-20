namespace Data.ViewModel.ChatRoom
{
	public class ChatRoomCreateModel
    {
        public string Name { get; set; } = null!;

        public string? Secret { get; set; }

        public bool IsPrivate { get; set; }

    }
}
