using Data.ViewModel.Message;

namespace Data.ViewModel.ChatRoom
{
	public class ChatDetailViewModel
	{
		public ChatRoomViewModel ChatRoomViewModel { get; set; }
		public List<MessageViewModel> MessageViewModel { get; set; } = new List<MessageViewModel>(); 

	}
}
