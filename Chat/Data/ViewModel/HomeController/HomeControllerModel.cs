using Data.ViewModel.ChatRoom;
using Data.ViewModel.User;

namespace Data.ViewModel.HomeController
{
	public class HomeControllerModel
	{
		public ProfileViewModel ProfileViewModel { get; set; } // Information account: avatar, name, email to show

		public ChatRoomCreateModel ChatRoomCreateModel { get; set; } // Feature create group

		public List<ChatRoomViewModel> ChatRooms { get; set; } = new List<ChatRoomViewModel>();// Show all chat room

		public ChatRoomCurrentViewModel? ChatRoomCurrentViewModel { get; set; } // Show one room with messages in this room

		public SendMessageViewModel SendMessageViewModel { get; set; } //Send message

		public JoinRoomModel JoinRoomViewModel { get; set; } // Join room

	}
}
