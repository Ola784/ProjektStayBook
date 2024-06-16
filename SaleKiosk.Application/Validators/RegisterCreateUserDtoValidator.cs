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
    public class RegisterCreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public RegisterCreateUserDtoValidator(IKioskUnitOfWork unitOfWork)
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(u => u.Username)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.PhoneNumber)
                 .NotEmpty();

            RuleFor(s => s.FirstName)
                .Custom((value, context) =>
                {
                    bool inUse = unitOfWork.UserRepository.IsInUse(value);
                    if (inUse)
                    {
                        context.AddFailure("Name", "Username is in use");
                    }
                });
        }
    }
}
