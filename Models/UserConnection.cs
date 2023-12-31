using System.ComponentModel.DataAnnotations;

namespace Chat_Service.Models
{
	public class UserConnection
	{
		[Required(ErrorMessage ="UserConnection requires an user ID")]
		public string UserId{ get; set; }

        [Required(ErrorMessage = "UserConnection requires an connection ID")]
        public string ConnectionId { get; set; }

		public UserConnection(string userId, string connectionId)
		{
			UserId = userId;
			ConnectionId = connectionId;
		}
	}
}

