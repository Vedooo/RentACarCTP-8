using Business.Constants.Message;
using Core.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage(Messages.NameCanNotBeBlank);
            RuleFor(u => u.LastName).NotEmpty().WithMessage(Messages.LastNameCanNotBeBlank);
            RuleFor(u => u.Email).NotEmpty().WithMessage(Messages.EmailCanNotBeBlank);
        }
    }
}
