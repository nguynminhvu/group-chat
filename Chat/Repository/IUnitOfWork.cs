using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public interface IUnitOfWork
	{
		public Task<int> SaveChanges();
		public IAccountRepository AccountRepository { get; }
		public IChatRoomRepository ChatRoomRepository { get; }
		public IUserRepository UserRepository { get; }
		public IChatRoomUserMappingRepository ChatRoomUserMappingRepository { get; }
		public IImageRepository ImageRepository { get; }
		public IImageSetRepository ImageSetRepository { get; }
		public IMessageRepository MessageRepository { get; }

	}
}
