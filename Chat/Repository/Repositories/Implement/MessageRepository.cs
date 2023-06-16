using Data.Entities;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implement
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public MessageRepository(ChatContext chatContext) : base(chatContext)
		{
		}
	}
}
