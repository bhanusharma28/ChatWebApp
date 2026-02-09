using Microsoft.EntityFrameworkCore;
using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Data
{
	public class ChatAppDbContext : DbContext
	{
		public ChatAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			
		}

		public DbSet<User> Users { get; set; }
		public DbSet<ChatRelation> ChatRelations { get; set; }
		public DbSet<Message> Messages { get; set; }
	}
}
