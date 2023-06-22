
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

		public async Task<ChatRoomCurrentViewModel> CreateChatRoom(string accId, ChatRoomCreateModel ccm)
		{
			ChatRoom chatRoom = new ChatRoom
			{
				Id = Guid.NewGuid().ToString(),
				Admin = accId,
				Creator = accId,
				IsActive = true,
				Secret = ccm.Secret,
				IsPrivate = ccm.IsPrivate,
				Name = ccm.Name,
				CreateDate = DateTime.Now,
				LastUpdate = DateTime.Now
			};
			await _uow.ChatRoomRepository.AddAsync(chatRoom);

			ChatRoomUserMapping chatRoomUserMapping = new ChatRoomUserMapping
			{
				Id = Guid.NewGuid().ToString(),
				AccId = accId,
				ChatRoomId = chatRoom.Id,
				IsActive = true
			};

			await _uow.ChatRoomUserMappingRepository.AddAsync(chatRoomUserMapping);

			return await _uow.SaveChanges() > 0 ? await GetChatRoom(chatRoom.Id) : null!;
		}

		public async Task<ChatRoomCurrentViewModel> GetChatRoom(string id)
		{
			var chatRoom = await _uow.ChatRoomRepository.FirstOrDefaultAsync(x => x.Id.Equals(id), x => x.Messages);
			return _mapper.Map<ChatRoomCurrentViewModel>(chatRoom);
		}

		public async Task<List<ChatRoomViewModel>> GetChatRooms(string accId)
		{
			var chatRooms = _uow.ChatRoomRepository.GetEntitiesPredicate(x => x.ChatRoomUserMappings.Any(x => x.AccId.Equals(accId)), x => x.Messages);
			if (chatRooms != null && (await chatRooms.ToListAsync()).Count > 0)
			{
				chatRooms = chatRooms.OrderByDescending(x => x.LastUpdate);
			}
			return await chatRooms.ProjectTo<ChatRoomViewModel>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<List<MessageViewModel>> GetMessages(string chatRoomId)
		{
			var rs = _uow.MessageRepository.GetEntitiesPredicate(x => x.ChatRoomId.Equals(chatRoomId)).OrderBy(x => x.CreateDate);
			return await rs.ProjectTo<MessageViewModel>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<List<MessageViewModel>> CreateMessage(SendMessageViewModel smvc)
		{
			var chatRoom = await _uow.ChatRoomRepository.FirstOrDefaultAsync(x => x.Id.Equals(smvc.RoomId));
			chatRoom.LastUpdate = DateTime.Now;
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

		public async Task<bool> CheckSecretKey(string roomId, string secret)
		{
			var room = await _uow.ChatRoomRepository.FirstOrDefaultAsync(x => x.Id.Equals(roomId));
			if (!string.IsNullOrEmpty(room.Secret))
			{
				return room.Secret.Equals(secret);
			}
			return true;
		}

		public async Task<ChatRoomCurrentViewModel> JoinRoom(JoinRoomModel joinRoomModel)
		{
			if (await CheckSecretKey(joinRoomModel.RoomId, joinRoomModel.AccountId))
			{
				ChatRoomUserMapping chatRoomUserMapping = new ChatRoomUserMapping
				{
					Id = Guid.NewGuid().ToString(),
					ChatRoomId = joinRoomModel.RoomId,
					AccId = joinRoomModel.AccountId,
					IsActive = true
				};
				await _uow.ChatRoomUserMappingRepository.AddAsync(chatRoomUserMapping);
				return await _uow.SaveChanges() > 0 ? await GetChatRoom(joinRoomModel.RoomId) : null!;
			}
			return null!;
		}
	}
}
