using Data.ViewModel.ChatRoom;
using Data.ViewModel.User;

namespace Data.ViewModel.HomeController
{
	public class HomeControllerModel
	{
		public ProfileViewModel ProfileViewModel { get; set; }
		public ChatRoomCreateModel ChatRoomCreateModel { get; set; }
		public List<ChatRoomViewModel> ChatRooms { get; set; }

	}
}
