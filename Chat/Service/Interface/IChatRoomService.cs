
using Data.ViewModel.ChatRoom;
using Data.ViewModel.Message;

namespace Service.Interface
{
	public interface IChatRoomService
	{
		public Task<ChatRoomCurrentViewModel> CreateChatRoom(string userId, ChatRoomCreateModel ccm);
		public Task<List<ChatRoomViewModel>> GetChatRooms(string creator);
		public Task<List<MessageViewModel>> GetMessages(string chatRoomId);
		public Task<List<MessageViewModel>> CreateMessage(SendMessageViewModel smvc);
		public Task<ChatRoomCurrentViewModel> GetChatRoom(string id);
		public Task<bool> CheckSecretKey(string roomId, string secret);
		public Task<ChatRoomCurrentViewModel> JoinRoom(JoinRoomModel joinRoomModel);
		public Task<ChatRoomCurrentViewModel> JoinRoomQR(string roomId, string accId);
	}
}
