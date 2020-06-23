using MailBoxSample.APIHelper.Validations;
using MailBoxSample.MailAPI.Models;
using FluentValidation;

namespace MailBoxSample.MailAPI.Validations
{
	public class MailModelValidator : BaseModelValidator<MailModel>
	{
		public MailModelValidator()
		{
			RuleFor(x=>x.Subject).MaximumLength(255);
			RuleFor(x=>x.FromUserId).GreaterThan(0);
			RuleFor(x=>x.ToUserId).GreaterThan(0);
		}
	}
}