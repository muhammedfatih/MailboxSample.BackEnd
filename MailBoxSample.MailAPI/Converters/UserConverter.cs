using MailBoxSample.APIHelper.Converters;
using MailBoxSample.MailAPI.Models;
using MailBoxSample.MailAPI.Entities;
using System;
using System.Collections.Generic;
using MailBoxSample.APIHelper.Builders;

namespace MailBoxSample.MailAPI.Converters
{
    public class UserConverter : BaseConverter<UserEntity, UserModel>
    {
        public new UserEntity Convert(UserModel model)
        {
            return new UserEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = new PasswordBuilder().Encrpyt(model.Password),
                Guid = model.Guid.ToString(),
                Token=model.Token,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
        public new UserModel Convert(UserEntity entity)
        {
            return new UserModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Password = entity.Password,
                Guid = new Guid(entity.Guid),
                Token = entity.Token,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
        public new List<UserModel> Convert(List<UserEntity> entities)
        {
            var models = new List<UserModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }
        public new List<UserEntity> Convert(List<UserModel> models)
        {
            var entities = new List<UserEntity>();
            foreach (var model in models)
                entities.Add(Convert(model));
            return entities;
        }
    }
}