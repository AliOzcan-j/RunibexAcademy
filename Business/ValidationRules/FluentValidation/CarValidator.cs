using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.SupplierId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.FuelTypeId).NotEmpty();
            RuleFor(c => c.DailyPrice).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(c => c.Transmission).NotEmpty();
            RuleFor(c => c.MilageLimit).NotEmpty();
        }
    }
}
