using Data.ViewModel.ChatRoom;
using Data.ViewModel.Message;
using Data.ViewModel.User;

namespace Data.ViewModel.HomeController
{
	public class HomeControllerModel
	{
		public ProfileViewModel ProfileViewModel { get; set; }
		public ChatRoomCreateModel ChatRoomCreateModel { get; set; }
		public List<ChatRoomViewModel> ChatRooms { get; set; } = new List<ChatRoomViewModel>();
		public List<MessageViewModel> MessageViewModel { get; set; } = new List<MessageViewModel>();
		public SendMessageViewModel SendMessageViewModel { get; set; }

	}
}
