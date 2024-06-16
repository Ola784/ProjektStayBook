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
    public class RegisterCreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        //public RegisterCreateProductDtoValidator()
        //{
        //    RuleFor(p => p.Name)
        //        .NotEmpty()
        //        .MinimumLength(2)
        //        .MaximumLength(20);

        //    RuleFor(p => p.Desc)
        //        .NotEmpty()
        //        .MinimumLength(2)
        //        .MaximumLength(20);

        //    RuleFor(p => p.UnitPrice)
        //        .GreaterThan(0);
        //}

        public RegisterCreateHotelDtoValidator(IKioskUnitOfWork unitOfWork)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(p => p.Desc)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(p => p.UnitPrice)
                .GreaterThan(0);

            RuleFor(s => s.Name)
                .Custom((value, context) =>
                {
                    bool inUse = unitOfWork.HotelRepository.IsInUse(value);
                    if (inUse)
                    {
                        context.AddFailure("Name", "Hotel name is in use");
                    }
                });
        }

    }
}
