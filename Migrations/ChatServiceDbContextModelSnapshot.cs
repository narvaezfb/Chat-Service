﻿// <auto-generated />
using System;
using Chat_Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat_Service.Migrations
{
    [DbContext(typeof(ChatServiceDbContext))]
    partial class ChatServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chat_Service.Models.Message", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId")
                        .IsUnique();

                    b.HasIndex("SenderId")
                        .IsUnique();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Chat_Service.Models.UserConnection", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("ConnectionId")
                        .IsUnique();

                    b.ToTable("UserConnections");
                });
#pragma warning restore 612, 618
        }
    }
}
