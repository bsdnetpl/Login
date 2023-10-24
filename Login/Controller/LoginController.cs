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
        private IValidator<UserValidation> _validator;
        private readonly IUserServices _userServices;

        public LoginController(IValidator<UserValidation> validator, IUserServices userServices)
        {
            _validator = validator;
            _userServices = userServices;
        }

        [HttpPost("AddUser")]
        public ActionResult AddUser(UserDto userDto)
        {
            ValidationResult result =  _validator.Validate(userDto);

            if (!result.IsValid)
            {
            }

            return Ok(_userServices.AddUser(userDto));
        }
    }
}
