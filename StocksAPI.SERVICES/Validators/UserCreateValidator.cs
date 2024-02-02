using FluentValidation;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateValidator()
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
            RuleFor(m => m.DisplayName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill user display name")
                .NotNull().WithMessage("Please fill display name");
            RuleFor(m => m.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill user role")
                .NotNull().WithMessage("Please fill user role")
                .Must(s => s != 0).WithMessage("Error in user role");
        }
    }
}
