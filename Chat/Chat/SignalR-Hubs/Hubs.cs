using Microsoft.AspNetCore.SignalR;
using Service.RegisService;

namespace Chat.SignalR_Hubs
{
	public class Hubs : Hub
	{
		private IRegisService _regisService;
		public Hubs(IRegisService regisService)
		{
			_regisService = regisService;
		}
		public async Task SendAll(string message, string email, string roomId, string timeChat)
		{
			await Clients.All.SendAsync("ReceiveMessage", message, email, roomId, timeChat);
		}

		//Send specification connectionId, store redis memory
		public async Task SendSpecification(string message, string email, string roomId, string timeChat, List<string> accIds, string accCurrent)
		{
			foreach (var accId in accIds)
			{
				if (!accId.Equals(accCurrent))
				{
					await Clients.Client(await GetConnection(accId)).SendAsync("Specification", message, email, roomId, timeChat);
				}
			}
		}
		public async Task<string> GetConnection(string accId)
		{
			return await _regisService.GetConnectionIdAsync(accId);
		}
		public async Task SaveConnectionId(string accId)
		{
			await _regisService.SaveConnectionIdString(accId, Context.ConnectionId);
		}


		//Send group, store SignalR memory
		public async Task AddConnectionGroupAsync(string roomId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
		}
		public async Task SendGroupAsync(string message, string email, string roomId, string timeChat)
		{
			await Clients.Group(roomId).SendAsync("GroupConnection", message, email, roomId, timeChat);
		}
	}
}
