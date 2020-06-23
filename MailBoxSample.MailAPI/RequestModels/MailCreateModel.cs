namespace MailBoxSample.MailAPI.RequestModels
{
    public class MailCreateModel
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ToUserName { get; set; }
    }
}
