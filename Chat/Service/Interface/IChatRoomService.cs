

using Data.ViewModel.ChatRoom;

namespace Service.Interface
{
    public interface IChatRoomService
    {
        public Task<ChatRoomViewModel> CreateChatRoom(string userId, ChatRoomCreateModel ccm);
    }
}
