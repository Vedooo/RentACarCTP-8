using Business.Constants.Message;
using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(2).WithMessage(Messages.CarNameMinLengthTwo);
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage(Messages.InvalidPriceValue);
        }
    }
}
