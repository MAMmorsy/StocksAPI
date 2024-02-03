using FluentValidation;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    public class InvoiceCreateValidator : AbstractValidator<InvoiceCreateDTO>
    {
        public InvoiceCreateValidator()
        {
            RuleFor(m => m)
               .NotNull().WithMessage("Please fill the required data");

            RuleFor(m => m.InvoiceNo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill InvoiceNo")
                .NotNull().WithMessage("Please fill InvoiceNo");
            RuleFor(m => m.InvoiceDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill InvoiceDate")
                .NotNull().WithMessage("Please fill InvoiceDate");
            RuleFor(m => m.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill UserId")
                .NotNull().WithMessage("Please fill UserId")
            .Must(s => s != 0).WithMessage("Error in UserId");
            RuleFor(m => m.Total)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Total")
                .NotNull().WithMessage("Please fill Total")
            .Must(s => s != 0).WithMessage("Error in Total");
            RuleFor(m => m.Taxes)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Taxes")
                .NotNull().WithMessage("Please fill Taxes")
            .Must(s => s != 0).WithMessage("Error in Taxes");
            RuleFor(m => m.Net)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill Net")
                .NotNull().WithMessage("Please fill Net")
            .Must(s => s != 0).WithMessage("Error in Net");
            RuleFor(m => m.TotalItems)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill TotalItems")
                .NotNull().WithMessage("Please fill TotalItems")
            .Must(s => s != 0).WithMessage("Error in TotalItems");
            RuleFor(m => m.TotalDiscount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please fill TotalDiscount")
                .NotNull().WithMessage("Please fill TotalDiscount");
            RuleForEach(m => m.items)
                .SetValidator(new InvoiceCreateItemValidator());
        }
    }
}
