using System;
using Microsoft.AspNetCore.SignalR;

namespace Chat_Service.Hubs
{
	public class ChatHub: Hub
	{
		//public override async Task OnConnectedAsync()
		//{
		//	await Clients.All.SendAsync("RecieveMessage", $"{Context.ConnectionId} has joined");
		//}
		public async Task SendMessage(string message)
		{
			await Clients.All.SendAsync("RecieveMessage", message);
		}
	}
}

