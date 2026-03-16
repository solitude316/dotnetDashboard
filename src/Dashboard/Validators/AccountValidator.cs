using FluentValidation;
using Dashboard.Dto;

namespace Dashboard.Validators;

public class AccountValidator : BaseValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("invalid_format");
        RuleFor(x => x.Email).MaximumLength(100).WithMessage("max_length_100");
        RuleFor(x => x.Password).NotEmpty().WithMessage("required");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("min_length_6");
        RuleFor(x => x.Password).MaximumLength(100).WithMessage("max_length_100");
    }
}