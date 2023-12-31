using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_Service.Models.ModelConfiguration
{
	public class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
	{
		public void Configure(EntityTypeBuilder<UserConnection> builder)
		{
			builder.HasKey(uc => uc.UserId);
			builder.Property(uc => uc.ConnectionId).IsRequired();

			builder.HasIndex(uc => uc.ConnectionId).IsUnique();
		}
	}
}

