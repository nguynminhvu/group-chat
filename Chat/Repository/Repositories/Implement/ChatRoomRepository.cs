using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implement
{
	public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
	{
		private ChatContext _chatContext;

		public ChatRoomRepository(ChatContext chatContext) : base(chatContext)
		{
			_chatContext = chatContext;
		}

		
	}
}
