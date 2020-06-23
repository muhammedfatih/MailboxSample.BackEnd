using MailBoxSample.APIHelper.Entities;

namespace MailBoxSample.MailAPI.Entities
{
	public class UserEntity : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
	}
}