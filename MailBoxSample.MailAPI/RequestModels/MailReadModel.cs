using System;

namespace MailBoxSample.MailAPI.RequestModels
{
    public class MailReadModel
    {
        public Guid Guid { get; set; }
        public string Subject { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public bool IsRead { get; set; }
    }
}
