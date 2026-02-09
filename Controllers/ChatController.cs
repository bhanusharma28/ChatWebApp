using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatWebAPI.Interfaces.IService;

namespace SignalRChatWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IChatService chatService;

		public ChatController(IChatService chatService)
		{
			this.chatService = chatService;
		}

		[HttpGet("history/{senderId}/{receiverId}")]
		public async Task<IActionResult> GetChatHistory(Guid senderId, Guid receiverId)
		{
			var messages = await chatService.GetChatHistoryAsync(senderId, receiverId);
			return Ok(messages);
		}
	}
}
