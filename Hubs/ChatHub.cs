using System;
using Chat_Service.Data;
using Chat_Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chat_Service.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatServiceDbContext _context;

        public ChatHub(ChatServiceDbContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                string userId = Context.GetHttpContext().Request.Query["userId"];

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception();
                }

                var connectionId = Context.ConnectionId;

                var userConnection = await _context.UserConnections.FindAsync(userId);

                if (userConnection != null)
                {
                    userConnection.ConnectionId = connectionId;
                }
                else
                {
                    UserConnection newUserConnection = new UserConnection(userId, connectionId);
                    await _context.UserConnections.AddAsync(newUserConnection);
                }
                await _context.SaveChangesAsync();

                await Clients.Clients(connectionId).SendAsync($"User: {userId} connected");

                await base.OnConnectedAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }  
        }

        public async Task SendMessageToUser(string senderId, string receiverId, string content)
        {
            try
            {
                var connection = await _context.UserConnections.FindAsync(receiverId);

                if (connection != null)
                {
                    await Clients.Client(connection.ConnectionId).SendAsync("ReceiveMessage", content);
                }
                else
                {
                    await Clients.Caller.SendAsync("ReceiveError", "The recipient is currently not connected.");
                }

                // Create and save the message to the database within a transaction
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Message message = new Message(senderId, receiverId, content);
                        _context.Add(message);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); 
                        throw; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

    }
}


