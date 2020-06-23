using FluentValidation;
using MailBoxSample.APIHelper.Converters;
using MailBoxSample.APIHelper.Models;
using MailBoxSample.MailAPI.Converters;
using MailBoxSample.MailAPI.Middleware;
using MailBoxSample.MailAPI.Models;
using MailBoxSample.MailAPI.RequestModels;
using MailBoxSample.MailAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailBoxSample.MailAPI.Controllers
{
    [Route("api/mail")]
    [AFAuthorization]
    public class MailController : BaseController
    {
        private readonly MailService _mailService;
        private readonly UserService _userService;
        private readonly AbstractValidator<MailModel> _mailModelValidator;
        private readonly ILogger _logger;
        public MailController(ILogger logger, MailService mailService, UserService userService, AbstractValidator<MailModel> mailModelValidator)
        {
            _logger = logger;
            _mailService = mailService;
            _userService = userService;
            _mailModelValidator = mailModelValidator;
        }
        [HttpPost]
        public ServiceResponse<MailCreateModel> Create([FromBody] MailCreateModel mailCreateRequestModel)
        {
            var response = new ServiceResponse<MailCreateModel>();

            var toUser = _userService.GetByUserName(mailCreateRequestModel.ToUserName);
            if (toUser.Errors.Count == 0)
            {
                var model = new MailModel();
                model.Content = mailCreateRequestModel.Content;
                model.Subject = mailCreateRequestModel.Subject;
                model.ToUserId = toUser.Data.ID;
                model.FromUserId = CurrentUser.ID;
                model.IsActive = true;
                model.IsDeleted = false;
                model.IsRead = false;

                var validationResult = _mailModelValidator.Validate(model);
                if (validationResult.IsValid)
                {
                    var entity = new MailConverter().Convert(model);
                    var serviceResponse = _mailService.Create(entity);
                    response.IsSuccessed = serviceResponse.IsSuccessed;
                    response.Errors = serviceResponse.Errors;
                    response.Data = new MailCreateModel()
                    {
                        Content = model.Content,
                        Subject = model.Subject,
                        ToUserName = toUser.Data.UserName
                    };
                }
                else
                {
                    _logger.Error("{source} {template} {logtype} {ValidationError}", "controller", "MailEntity", "validationerror", validationResult.Errors);
                    response.IsSuccessed = false;
                    response.Errors = new ValidationFailureConverter().Convert(validationResult.Errors.ToList());
                }
            }
            else
            {
                response.Errors.Add(new ServiceError() { Code= "200003", InnerMessage= "To username is not found.", Message="To username is not found." });
            }
            return response;
        }

        [HttpGet("{guid}")]
        public ServiceResponse<MailReadModel> Read(Guid guid)
        {
            var response = new ServiceResponse<MailReadModel>();
            var entity = _mailService.Read(guid);
            if (entity.IsSuccessed)
            {
                var mailConverter = new MailConverter();
                var responseData = mailConverter.Convert(entity.Data);
                responseData.FromUser = new UserConverter().Convert(_userService.Read(responseData.FromUserId).Data);
                responseData.ToUser = new UserConverter().Convert(_userService.Read(responseData.ToUserId).Data);
                response.Data = mailConverter.ConvertReadModel(responseData);
                if (!responseData.IsRead || responseData.ToUserId == CurrentUser.ID)
                {
                    responseData.IsRead = true;
                    var entityData = new MailConverter().Convert(responseData);
                    _mailService.Update(entityData);
                }
            }
            response.IsSuccessed = response.IsSuccessed;
            return response;
        }
        [HttpGet("unreads")]
        public ServiceResponse<List<MailReadModel>> ListUnreadMails()
        {
            var response = new ServiceResponse<List<MailReadModel>>();
            var serviceResponse = _mailService.ListUnreadMails(CurrentUser.ID);
            var mailConverter = new MailConverter();
            if (serviceResponse.IsSuccessed) response.Data = mailConverter.ConvertReadModel(mailConverter.Convert(serviceResponse.Data));
            response.IsSuccessed = serviceResponse.IsSuccessed;
            return response;
        }
        [HttpGet("numberOfUnreads")]
        public ServiceResponse<int> NumberOfUNreadMessages()
        {
            var response = new ServiceResponse<int>();
            var serviceResponse = _mailService.ListUnreadMails(CurrentUser.ID);
            if (serviceResponse.IsSuccessed) response.Data = serviceResponse.Data.Count;
            response.IsSuccessed = serviceResponse.IsSuccessed;
            return response;
        }
        [HttpGet("{page}/{pageSize}")]
        public ServiceResponse<List<MailReadModel>> List(int page, int pageSize)
        {
            var response = new ServiceResponse<List<MailReadModel>>();
            var mailConverter = new MailConverter();
            var serviceResponse = _mailService.ListForUser(page, pageSize, CurrentUser.ID);
            if (serviceResponse.IsSuccessed) response.Data = mailConverter.ConvertReadModel(mailConverter.Convert(serviceResponse.Data));
            response.IsSuccessed = serviceResponse.IsSuccessed;
            return response;
        }
    }
}