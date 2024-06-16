using FluentValidation;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.SharedKernel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Application.Validators
{
    public class RegisterCreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
    {
        public RegisterCreateReviewDtoValidator(IKioskUnitOfWork unitOfWork)
        {
            RuleFor(u => u.Rate)
                .NotNull()
                .WithMessage("Rate cannot be empty")
                .InclusiveBetween(1, 5)
                .WithMessage("Rate must be between 1 and 5");
        }

    }
}
