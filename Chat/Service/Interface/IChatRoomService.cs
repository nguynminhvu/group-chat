
using Data.ViewModel.ChatRoom;
using Data.ViewModel.Message;

namespace Service.Interface
{
	public interface IChatRoomService
	{
		public Task<ChatRoomViewModel> CreateChatRoom(string userId, ChatRoomCreateModel ccm);
		public Task<List<ChatRoomViewModel>> GetChatRooms(string creator);
		public Task<List<MessageViewModel>> GetMessages(string chatRoomId);
		public Task<List<MessageViewModel>> CreateMessage(SendMessageViewModel smvc);
	}
}
