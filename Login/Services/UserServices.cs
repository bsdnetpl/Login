using AutoMapper;
using Login.DB;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ContextModel _contextModel;
        private readonly AuthenticationSttetings _authenticationSttetings;



        public UserServices(IMapper mapper, IPasswordHasher<User> passwordHasher, ContextModel contextModel, AuthenticationSttetings authenticationSttetings)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _contextModel = contextModel;
            _authenticationSttetings = authenticationSttetings;
        }

        public async Task<bool> AddUser(UserDto userDto)
        {
            var result = _mapper.Map<User>(userDto);
            result.Id = Guid.NewGuid();
            result.DateCreate = DateTime.Now;
            result.Password = _passwordHasher.HashPassword(result, userDto.Password);
           // result.Role = "Admin";
            await _contextModel.Users.AddAsync(result);
            await _contextModel.SaveChangesAsync();
            return true;
        }
        public async Task<string> Login(string Email, string Password)
        {
            var EmailResult = await _contextModel.Users.Where(f => f.Email == Email).FirstOrDefaultAsync();
            if (EmailResult is null)
            {
                return "Bad address or password";
            }
            var ResPassw = _passwordHasher.VerifyHashedPassword(EmailResult, EmailResult.Password, Password);
            if (ResPassw == PasswordVerificationResult.Failed)
            {
                return "Bad address or password";
            }
            var claims = new List<Claim>()
            {
             new Claim(ClaimTypes.NameIdentifier, EmailResult.Id.ToString()),
             new Claim(ClaimTypes.Name, $"{EmailResult.Name} {EmailResult.LastName}"),
             new Claim(ClaimTypes.Role,  $"{EmailResult.Role}"),
             new Claim("DateCreation",  EmailResult.DateCreate.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSttetings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_authenticationSttetings.JwtExpireDays));
            var token = new JwtSecurityToken(
                _authenticationSttetings.JwtIssuer,
                _authenticationSttetings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
