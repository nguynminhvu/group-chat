using Data.Entities;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implement
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ChatContext chatContext) : base(chatContext)
		{
		}
	}
}
