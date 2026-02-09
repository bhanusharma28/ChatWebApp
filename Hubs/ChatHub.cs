using Microsoft.AspNetCore.SignalR;
using SignalRChatWebAPI.Interfaces.IService;

namespace SignalRChatWebAPI.Hubs
{
	public class ChatHub : Hub
	{
		private readonly IChatService chatService;

		public ChatHub(IChatService chatService)
		{
			this.chatService = chatService;
		}

		public async Task UserConnected(Guid userId)
		{
			await chatService.SetUserOnline(userId, true);

			await Clients.All.SendAsync("UserStatusChanged", userId, true);
		}

		public async Task UserDisconnected(Guid userId)
		{
			await chatService.SetUserOnline(userId, false);

			await Clients.All.SendAsync("UserStatusChanged", userId, false);
		}

		public async Task SendMessage(Guid senderId, Guid receiverId, string message)
		{
			await chatService.ProcessMessage(senderId, receiverId, message);
			await Clients.All.SendAsync("ReceiveMessage", senderId, message);
		}

	}
}
