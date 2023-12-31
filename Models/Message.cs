using System.ComponentModel.DataAnnotations;

namespace Chat_Service.Models
{
	public class Message
	{
		[Required(ErrorMessage ="Message ID is required")]
		public string MessageId { get; set; }

        [Required(ErrorMessage = "Sender ID is required")]
        public string SenderId { get; set; }

        [Required(ErrorMessage = "Receiver ID is required")]
        public string ReceiverId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

		public DateTime Timestamp { get; set; }

		public Message(string senderId, string receiverId, string content)
		{
			MessageId = Guid.NewGuid().ToString();
			SenderId = senderId;
			ReceiverId = receiverId;
			Content = content;
        }
	}
}

