using FluentValidation;

namespace Login.Models
{
    public class UserDto
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Phone { set; get; }
    }
    public class UserValidation : AbstractValidator<UserDto>
    {
        public UserValidation()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Bad data or string is not email address!");
            RuleFor(x => x.Password).MinimumLength(15).MaximumLength(32).NotNull()
            .NotEmpty().WithMessage("Bad data ! or minimal char 15 and maximum length 32")
            .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
            .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
            .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.")
            .Matches(@"[""!@$%^&*(){}:;<>,.?/+\-_=|'[\]~\\]").WithMessage("' must contain one or more special characters.");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Bad data !");
        }
    }
}
