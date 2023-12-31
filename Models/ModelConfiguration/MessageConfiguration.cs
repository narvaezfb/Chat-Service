using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_Service.Models.ModelConfiguration
{
	public class MessageConfiguration: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.MessageId);
            builder.Property(m => m.SenderId).IsRequired();
            builder.Property(m => m.ReceiverId).IsRequired();
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Timestamp);

            //Indexes 
            builder.HasIndex(m => m.SenderId).IsUnique();
            builder.HasIndex(m => m.ReceiverId).IsUnique();

            //Timestamp 
            builder.Property(m => m.Timestamp).HasColumnType("timestamp with time zone");  
        }
    }
}


