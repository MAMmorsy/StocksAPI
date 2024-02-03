using FluentValidation;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    public class InvoiceCreateItemValidator : AbstractValidator<InvoiceItemsCreateDTO>
    {
        public InvoiceCreateItemValidator()
        {
            RuleFor(m => m)
               .NotNull().WithMessage("Please fill the required data");

            RuleFor(m => m.StoreProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill StoreProductId")
                .NotNull().WithMessage("Please fill StoreProductId")
            .Must(s => s != 0).WithMessage("Error in StoreProductId");
            RuleFor(m => m.Quantity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Quantity")
                .NotNull().WithMessage("Please fill Quantity")
            .Must(s => s != 0).WithMessage("Error in Quantity");
            RuleFor(m => m.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Price")
                .NotNull().WithMessage("Please fill Price")
            .Must(s => s != 0).WithMessage("Error in Price");
            RuleFor(m => m.Total)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Total")
                .NotNull().WithMessage("Please fill Total")
            .Must(s => s != 0).WithMessage("Error in Total");
            RuleFor(m => m.Net)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Net")
                .NotNull().WithMessage("Please fill Net")
            .Must(s => s != 0).WithMessage("Error in Net");
            RuleFor(m => m.Discount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Discount")
                .NotNull().WithMessage("Please fill Discount");
        }
    }
}
