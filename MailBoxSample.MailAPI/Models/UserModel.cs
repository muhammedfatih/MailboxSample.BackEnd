using MailBoxSample.APIHelper.Models;

namespace MailBoxSample.MailAPI.Models
{
	public class UserModel : BaseModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
	}
}