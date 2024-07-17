using FluentValidation;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;

namespace SU24SE069_PLATFORM_KAROKE_Service.Validator
{
    public class MemberSignUpRequestValidator : AbstractValidator<MemberSignUpRequest>
    {
        public MemberSignUpRequestValidator()
        {
            RuleFor(e => e.Email)
                .Must(value => !string.IsNullOrEmpty(value)).WithMessage("Email address cannot be empty!")
                .EmailAddress().WithMessage("Email address is not valid!")
                .MaximumLength(254).WithMessage("Email address cannot exceed 254 characters!");

            RuleFor(e => e.UserName)
                .Must(value => !string.IsNullOrEmpty(value)).WithMessage("Username cannot be empty!")
                .MinimumLength(6).WithMessage("Username must contains at least 6 characters")
                .MaximumLength(15).WithMessage("Username length cannot exceed 15 characters")
                .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must contain only alphanumeric characters and no empty space");

            RuleFor(e => e.Password)
                .Must(value => !string.IsNullOrEmpty(value)).WithMessage("Password cannot be empty!")
                .MinimumLength(6).WithMessage("Password must contains at least 6 characters")
                .MaximumLength(32).WithMessage("Password length cannot exceed 32 characters")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
                .WithMessage("Password must contain at least 1 alphabet character, 1 numeric character, 1 special character and no empty space!");
        }
    }
}
