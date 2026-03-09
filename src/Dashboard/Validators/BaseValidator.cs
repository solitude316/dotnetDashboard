using FluentValidation;
using Dashboard.Dto;
using FluentValidation.Results;

namespace Dashboard.Validators;

public class BaseValidator<T> : AbstractValidator<T>
{
    public BaseValidator()
    {
        
    }

    public Dictionary<string, string> ExtractErrors(ValidationResult result)
    {
        var errors = new Dictionary<string, string>();

        foreach(var error in result.Errors)
        {
            if (!errors.ContainsKey(error.PropertyName))
            {
                errors[error.PropertyName] = error.ErrorMessage;
            }
        }

        return errors;
    }
}