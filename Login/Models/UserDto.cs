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
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(20).NotNull().NotEmpty().WithMessage("Bad data ! or minimal char 8 and maximum lenght 20");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Bad data !");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Bad data !");
        }
    }
}
