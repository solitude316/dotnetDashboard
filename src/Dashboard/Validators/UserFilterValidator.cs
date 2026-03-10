
using Dashboard.Dto;

namespace Dashboard.Validators;

public class UserFilterValidator : BaseValidator<UserFilterDto>
{
    public UserFilterValidator()
    {
        // RuleFor(x => x.FirstName).Length(6, 30).WithMessage("length_between_6_and_20");
        // RuleFor(x => x.LastName).Length(2, 30).WithMessage("length_between_2_and_30");
        // RuleFor(x => x.Gender).IsInEnum().WithMessage("invalid_enum_value");
        // RuleFor(x => x.Birthday).NotNull().WithMessage("required");
    }
}