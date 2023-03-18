using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.DAL.Dtos;

namespace TheAddress.BLL.Validations
{
    public class PropertyValidation : AbstractValidator<PropertyDto>
    {
        public PropertyValidation()
        {
            RuleFor(d => d.Name).NotNull().WithMessage("{PropertyName} daxil edin")
                .NotEmpty().WithMessage("{PropertyName} daxil edin");
            RuleFor(d => d.Address).NotNull().WithMessage("{PropertyName} daxil edin")
                  .NotEmpty().WithMessage("{PropertyName} daxil edin");
            RuleFor(d => d.Price).NotNull().WithMessage("{PropertyName} daxil edin")
                  .NotEmpty().WithMessage("{PropertyName} daxil edin");
        }
    }
}
