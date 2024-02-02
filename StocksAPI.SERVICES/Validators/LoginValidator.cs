using StocksAPI.CORE.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(m => m)
               .NotNull().WithMessage("Please fill the required data");

            RuleFor(m => m.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill username")
                .NotNull().WithMessage("Please fill username");
            RuleFor(m => m.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill password")
                .NotNull().WithMessage("Please fill password");
        }
    }
}
