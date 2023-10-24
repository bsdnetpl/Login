using Login.Models;

namespace Login.Services
{
    public interface IUserServices
    {
        Task<bool> AddUser(UserDto userDto);
        Task<string> Login(string Email, string Password);
    }
}