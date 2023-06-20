

using AutoMapper;
using Data.Entities;
using Data.ViewModel.ChatRoom;
using Repository;
using Repository.Repositories.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class ChatRoomService : IChatRoomService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public ChatRoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ChatRoomViewModel> CreateChatRoom(string accId, ChatRoomCreateModel ccm)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.AccId.Equals(accId));
            if (user != null)
            {
                ChatRoom chatRoom = new ChatRoom
                {
                    Id = Guid.NewGuid().ToString(),
                    Admin = user.Id,
                    Creator = user.Id,
                    IsActive = true,
                    Secret = ccm.Secret,
                    IsPrivate = ccm.IsPrivate,
                    Name = ccm.Name
                };
                await _uow.ChatRoomRepository.AddAsync(chatRoom);
                return await _uow.SaveChanges() > 0 ? await GetChatRoom(chatRoom.Id) : null!;
            }
            return null!;
        }
        public async Task<ChatRoomViewModel> GetChatRoom(string id)
        {
            var chatRoom = await _uow.ChatRoomRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return _mapper.Map<ChatRoomViewModel>(chatRoom);
        }
    }
}
