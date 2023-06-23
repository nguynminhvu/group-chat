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
		public async Task SaveConnectionId(string accId)
		{
			await _regisService.SaveConnectionIdString(Context.ConnectionId, accId);
		}

	}
}
