using FluentValidation;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    internal class UserSelectValidator : AbstractValidator<UserSearchDTO>
    {
        public UserSelectValidator()
        {
            RuleFor(m => m)
                   .NotNull().WithMessage("Please fill the required data");
            RuleFor(m => m.UserId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please fill user id")
                    .NotNull().WithMessage("Please fill user id")
                    .Must(s => s != 0).WithMessage("Error in user id");
        }
        }
}
