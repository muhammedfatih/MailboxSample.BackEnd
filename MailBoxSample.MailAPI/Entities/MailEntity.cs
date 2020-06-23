using MailBoxSample.APIHelper.Entities;

namespace MailBoxSample.MailAPI.Entities
{
	public class MailEntity : BaseEntity
	{
		public string Subject { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public int FromUserId { get; set; }
		public int ToUserId { get; set; }
	}
}