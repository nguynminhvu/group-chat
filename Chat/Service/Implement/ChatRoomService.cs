

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.ViewModel.ChatRoom;
using Data.ViewModel.Message;
using Microsoft.EntityFrameworkCore;
using Repository;
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
			ChatRoom chatRoom = new ChatRoom
			{
				Id = Guid.NewGuid().ToString(),
				Admin = accId,
				Creator = accId,
				IsActive = true,
				Secret = ccm.Secret,
				IsPrivate = ccm.IsPrivate,
				Name = ccm.Name
			};
			await _uow.ChatRoomRepository.AddAsync(chatRoom);
			return await _uow.SaveChanges() > 0 ? await GetChatRoom(chatRoom.Id) : null!;
		}

		public async Task<ChatRoomViewModel> GetChatRoom(string id)
		{
			var chatRoom = await _uow.ChatRoomRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
			return _mapper.Map<ChatRoomViewModel>(chatRoom);
		}

		public async Task<List<ChatRoomViewModel>> GetChatRooms(string creator)
		{
			return await _uow.ChatRoomRepository.GetEntitiesPredicate(x => x.Creator.Equals(creator)).ProjectTo<ChatRoomViewModel>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<List<MessageViewModel>> GetMessages(string chatRoomId)
		{
			var rs = _uow.MessageRepository.GetEntitiesPredicate(x => x.ChatRoomId.Equals(chatRoomId)).OrderBy(x=>x.CreateDate);
			return await rs.ProjectTo<MessageViewModel>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<List<MessageViewModel>> CreateMessage(SendMessageViewModel smvc)
		{
			Message message = new Message
			{
				Id = Guid.NewGuid().ToString(),
				ChatRoomId = smvc.RoomId,
				Sender = smvc.SenderId,
				CreateDate = DateTime.Now,
				Message1 = smvc.Message1
			};
			await _uow.MessageRepository.AddAsync(message);
			return await _uow.SaveChanges() > 0 ? await GetMessages(smvc.RoomId) : null!;
		}
	}
}
