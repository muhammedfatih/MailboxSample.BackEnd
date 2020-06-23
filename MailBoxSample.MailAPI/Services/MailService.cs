using MailBoxSample.APIHelper.Services;
using MailBoxSample.MailAPI.Entities;
using MailBoxSample.MailAPI.Repositories;
using FluentValidation;
using Serilog;
using System;
using MailBoxSample.APIHelper.Models;
using MailBoxSample.APIHelper.Builders;
using System.Collections.Generic;

namespace MailBoxSample.MailAPI.Services
{
	public class MailService : BaseService<MailEntity>
	{
        protected MailRepository _repository;
        public MailService(ILogger logger, MailRepository repository) : base(logger, repository)
        {
            _repository = repository;
        }

        public ServiceResponse<List<MailEntity>> ListForUser(int page, int pageSize, int toUserId)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<List<MailEntity>>();
            var obj = _repository.ListForUser(page, pageSize, toUserId);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "200001", Message = "Received mails can not list.", InnerMessage = "Received mails can not list." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
        public ServiceResponse<List<MailEntity>> ListUnreadMails(int toUserId)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<List<MailEntity>>();
            var obj = _repository.ListUnreadMails(toUserId);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "200002", Message = "Unread mails can not list.", InnerMessage = "Unread mails can not list." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
    }
}