using FluentValidation;
using MailBoxSample.APIHelper.Models;
using System;

namespace MailBoxSample.APIHelper.Validations
{
    public class BaseModelValidator<T> : AbstractValidator<T>
    where T : BaseModel
    {
        public BaseModelValidator()
        {
            When(x => x.Guid.Equals(new Guid()), () =>
            {
                RuleFor(x => x.CreatedAt).NotNull();
            });
        }
    }
}
