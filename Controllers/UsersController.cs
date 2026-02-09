using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChatWebAPI.Data;
using SignalRChatWebAPI.Interfaces.IService;
using SignalRChatWebAPI.Services;

namespace SignalRChatWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService userService;

		public UsersController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await userService.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("byname/{name}")]
		public async Task<IActionResult> GetUserByName(string name)
		{
			var user = await userService.GetUserByNameAsync(name);

			if (user == null)
				return NotFound($"User '{name}' not found");

			return Ok(user);
		}
	}
}
