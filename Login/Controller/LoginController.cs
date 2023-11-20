using FluentValidation;
using FluentValidation.Results;
using Login.Models;
using Login.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Login.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IValidator<UserDto> _validator;
        private readonly IUserServices _userServices;

        public LoginController(IValidator<UserDto> validator, IUserServices userServices)
        {
            _validator = validator;
            _userServices = userServices;
        }

        [HttpPost("AddUser")]
       [Authorize (Roles="Admin")]
        public async Task<ActionResult> AddUser(UserDto userDto)
        {
            ValidationResult result = await  _validator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                return StatusCode(300,"Wrong Data");
            }

            return Ok(await _userServices.AddUser(userDto));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>>Login([FromBody] login login)
        {
            return Ok(await _userServices.Login(login.email, login.password));
        }
    }
}
