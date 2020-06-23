using MailBoxSample.APIHelper.Converters;
using MailBoxSample.MailAPI.Entities;
using MailBoxSample.MailAPI.Models;
using MailBoxSample.MailAPI.RequestModels;
using System;
using System.Collections.Generic;

namespace MailBoxSample.MailAPI.Converters
{
    public class MailConverter : BaseConverter<MailEntity, MailModel>
    {
        public new MailEntity Convert(MailModel model)
        {
            return new MailEntity()
            {
                Subject = model.Subject,
                Content = model.Content,
                IsRead = model.IsRead,
                FromUserId = model.FromUserId,
                ToUserId = model.ToUserId,
                Guid = model.Guid.ToString(),
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
        public new MailModel Convert(MailEntity entity)
        {
            return new MailModel()
            {
                Subject = entity.Subject,
                Content = entity.Content,
                IsRead = entity.IsRead,
                FromUserId = entity.FromUserId,
                ToUserId = entity.ToUserId,
                Guid = new Guid(entity.Guid),
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
        public new List<MailModel> Convert(List<MailEntity> entities)
        {
            var models = new List<MailModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }
        public new List<MailEntity> Convert(List<MailModel> models)
        {
            var entities = new List<MailEntity>();
            foreach (var model in models)
                entities.Add(Convert(model));
            return entities;
        }
        public MailReadModel ConvertReadModel(MailModel model)
        {
            return new MailReadModel()
            {
                Guid = model.Guid,
                Subject = model.Subject,
                Summary = model.Content.Substring(0, Math.Min(model.Content.Length, 20)),
                Content = model.Content,
                CreatedAt = model.CreatedAt,
                FromUserName = model.FromUser.UserName,
                ToUserName = model.ToUser.UserName,
                IsRead = model.IsRead
            };
        }
        public List<MailReadModel> ConvertReadModel(List<MailModel> models)
        {
            var entities = new List<MailReadModel>();
            foreach (var model in models)
                entities.Add(ConvertReadModel(model));
            return entities;
        }
    }
}