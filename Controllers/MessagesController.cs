using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chat_Service.Models;
using Chat_Service.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat_Service.Controllers
{
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ChatServiceDbContext _context;

        public MessagesController(ChatServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetMessage/{messageId}")]
        public async Task<IActionResult> GetOneMessage(string messageId)
        {
            try
            {
                var message = await _context.Messages.FindAsync(messageId);

                if(message == null)
                {
                    return BadRequest("Not message found with that ID");
                }

                return Ok(message);

            }catch(Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpGet("GetUsersConversation/{user1}/{user2}")]
        public async Task<IActionResult> GetUsersChat(string user1, string user2)
        {
            try
            {
                if (string.IsNullOrEmpty(user1) || string.IsNullOrEmpty(user2))
                {
                    return BadRequest("Invalid arguments on the request");
                }
                
                var messages = await _context.Messages.Where(m => (m.SenderId == user1 && m.ReceiverId == user2) ||
                                                                  (m.SenderId == user2 && m.ReceiverId == user1))
                                                      .OrderByDescending(m => m.Timestamp)
                                                      .ToListAsync();

                if(messages.Count <= 0)
                {
                    return BadRequest("Not messages found for these users");
                }

                return Ok(messages);

            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}

