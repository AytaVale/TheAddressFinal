using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.DAL.Dtos;

namespace TheAddress.BLL.Validations
{
    public class PropertyCategoryValidation : AbstractValidator<PropertyCategoryDto>
    {
        public PropertyCategoryValidation()
        {
            RuleFor(d => d.Name).NotNull().WithMessage("{PropertyName} daxil edin")
                .NotEmpty().WithMessage("{PropertyName} edin");
        }
    }
}
