using StocksAPI.CORE.Models.DTOs;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Validators
{
    public class ValidatorHandler
    {
        public static List<Error>? Validate<M>(M model, AbstractValidator<M> validator) where M : class
        {
            ValidationResult validationResult = validator.Validate(model);
            List<Error>? Errors = new List<Error>();
            if (!validationResult.IsValid)
            {
                Errors = ErrorReturn(validationResult);
            }
            return Errors;
        }
        private static List<Error> ErrorReturn(ValidationResult validationResult)
        {
            List<Error> errors = new List<Error>();
            List<ValidationFailure> errores = new List<ValidationFailure>();
            foreach (var error in validationResult.Errors)
            {
                errores.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
            }
            errors = errores.Select(x => new Error() { ErrorMessage = x.ErrorMessage }).ToList();
            return errors;
        }
    }
}
