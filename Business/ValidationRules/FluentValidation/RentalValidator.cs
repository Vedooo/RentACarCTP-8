using Business.Constants.Message;
using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(p => p.CarId).NotEmpty().WithMessage(Messages.PleaseEnterValidCarId);
            RuleFor(p => p.CustomerId).NotEmpty().WithMessage(Messages.PleaseEnterValidCustomerId);
            RuleFor(p => p.RentDate).NotEmpty().WithMessage(Messages.RentDateCanNotBeBlank);
            RuleFor(p => p.ReturnDate).NotEmpty().WithMessage(Messages.ReturnDateCanNotBeBlank);
            
        }
    }
}
