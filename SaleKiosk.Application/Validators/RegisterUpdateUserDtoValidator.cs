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
    public class RegisterUpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public RegisterUpdateUserDtoValidator(IKioskUnitOfWork unitOfWork)
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(u => u.LastName)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(20);

            RuleFor(u => u.PhoneNumber)
                 .NotEmpty();

        }

    }
}
