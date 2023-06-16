using Data.Entities;
using Repository.Repositories.Implement;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ChatContext _context;
		public IAccountRepository AccountRepository { get; private set; }

		public IChatRoomRepository ChatRoomRepository { get; private set; }

		public IUserRepository UserRepository { get; private set; }

		public IChatRoomUserMappingRepository ChatRoomUserMappingRepository { get; private set; }

		public IImageRepository ImageRepository { get; private set; }

		public IImageSetRepository ImageSetRepository { get; private set; }

		public IMessageRepository MessageRepository { get; private set; }

		public UnitOfWork(ChatContext chatContext)
		{
			_context = chatContext;
			AccountRepository = new AccountRepository(_context);
			ChatRoomRepository= new ChatRoomRepository(_context);
			UserRepository = new UserRepository(_context);
			ChatRoomUserMappingRepository = new ChatRoomUserMappingRepository(_context);
			ImageRepository= new ImageRepository(_context);
			ImageSetRepository = new ImageSetRepository(_context);
			MessageRepository = new MessageRepository(_context);
		}

		public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
