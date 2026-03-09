using FluentValidation;
using Dashboard.Dto;

namespace Dashboard.Validators;

public class UserRegistValidator : BaseValidator<RegistDto>
{
    public UserRegistValidator()
    {
        RuleFor(x => x.email).NotEmpty().WithMessage("required");
        RuleFor(x => x.email).EmailAddress().WithMessage("invalid_format");
        RuleFor(x => x.email).MaximumLength(100).WithMessage("max_length_100");
        RuleFor(x => x.password).NotEmpty().WithMessage("required");
        RuleFor(x => x.password).MinimumLength(6).WithMessage("min_length_6");
        RuleFor(x => x.password).MaximumLength(100).WithMessage("max_length_100");
    }
}