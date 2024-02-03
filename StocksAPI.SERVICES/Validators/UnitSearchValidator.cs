using FluentValidation;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    internal class UnitSearchValidator : AbstractValidator<UnitSearchDTO>
    {
        public UnitSearchValidator()
        {
            RuleFor(m => m)
                   .NotNull().WithMessage("Please fill the required data");
            RuleFor(m => m.ProductId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please fill product id")
                    .NotNull().WithMessage("Please fill product id")
                    .Must(s => s != 0).WithMessage("Error in product id");
            RuleFor(m => m.StoreId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please fill store id")
                    .NotNull().WithMessage("Please fill store id")
                    .Must(s => s != 0).WithMessage("Error in store id");
        }
    }
}
