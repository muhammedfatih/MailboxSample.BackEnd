using FluentValidation.TestHelper;
using MailBoxSample.APIHelper.Models;
using MailBoxSample.APIHelper.Validations;
using NUnit.Framework;
using System;

namespace MailBoxSample.APIHelper.Tests.Validations
{
    public class BaseEntityValidatorTest
    {
        internal class SampleModel : BaseModel
        {
        }

        [Test]
        public void ItShouldNotReturnValidationErrorWhenCreatedAtIsPositive()
        {
            new BaseModelValidator<SampleModel>().ShouldNotHaveValidationErrorFor(t => t.CreatedAt, DateTime.Now);
        }
    }
}
