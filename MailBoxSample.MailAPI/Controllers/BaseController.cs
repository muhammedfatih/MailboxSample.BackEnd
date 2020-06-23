using MailBoxSample.MailAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MailBoxSample.MailAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        public UserEntity CurrentUser { get; set; }
        public BaseController()
        {
            CurrentUser = new UserEntity();
        }
    }
}