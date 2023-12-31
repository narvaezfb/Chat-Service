using System;
using Chat_Service.Models;
using Chat_Service.Models.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Chat_Service.Data
{
	public class ChatServiceDbContext: DbContext
	{
		public DbSet<UserConnection> UserConnections { get; set; }
		public DbSet<Message> Messages { get; set; }

		public ChatServiceDbContext(DbContextOptions<ChatServiceDbContext> options)
		: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConnectionConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }

    }
}

