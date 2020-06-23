using MailBoxSample.APIHelper.Models;

namespace MailBoxSample.MailAPI.Models
{
	public class MailModel : BaseModel
	{
		public string Subject { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public int FromUserId { get; set; }
		public UserModel FromUser { get; set; }
		public int ToUserId { get; set; }
		public UserModel ToUser { get; set; }
		public MailModel () {
			FromUser = new UserModel();
			ToUser = new UserModel();
		}
	}
}