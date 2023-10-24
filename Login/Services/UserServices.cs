using AutoMapper;
using Login.DB;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Login.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ContextModel _contextModel;



        public UserServices(IMapper mapper, IPasswordHasher<User> passwordHasher, ContextModel contextModel)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _contextModel = contextModel;
        }

        public async Task<bool> AddUser(UserDto userDto)
        {
            var result = _mapper.Map<User>(userDto);
            result.Id = Guid.NewGuid();
            result.DateCreate = DateTime.Now;
            result.Password = _passwordHasher.HashPassword(result, userDto.Password);
            await _contextModel.Users.AddAsync(result);
            await _contextModel.SaveChangesAsync();
            return true;
        }
        public async Task<string> Login(string Email, string Password)
        {
            var EmailResult = await _contextModel.Users.Where(f => f.Email == Email).FirstOrDefaultAsync();
            if (EmailResult is null)
            {
                return "Bad Address or password";
            }
            var ResPassw = _passwordHasher.VerifyHashedPassword(EmailResult, EmailResult.Password, Password);
            if (ResPassw == PasswordVerificationResult.Failed)
            {
                return "Bad Address or password";
            }
            return "You are Login !";
        }
    }
}
