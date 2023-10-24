using FluentValidation;
using FluentValidation.Results;
using Login.Models;
using Login.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Login.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IValidator<UserDto> _validator;
        private readonly IUserServices _userServices;

        public LoginController(IValidator<UserDto> validator, IUserServices userServices)
        {
            _validator = validator;
            _userServices = userServices;
        }

        [HttpPost("AddUser")]
        public ActionResult AddUser(UserDto userDto)
        {
            ValidationResult result =  _validator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                return StatusCode(300,"Wron Data");
            }

            return Ok(_userServices.AddUser(userDto));
        }
    }
}
