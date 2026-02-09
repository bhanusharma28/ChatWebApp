using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Interfaces.IService
{
	public interface IUserService
	{
		Task<User?> GetUserByNameAsync(string name);
		Task<List<User>> GetAllUsersAsync();
	}
}
