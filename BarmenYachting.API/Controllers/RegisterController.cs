using BarmenYachting.Implementation;
using BarmenYachting.Application.Logging;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BarmenYachting.Api.Extensions;
using System;

namespace BarmenYachting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseHandler _handler;
        private readonly IRegisterUserCommand _command;
        private readonly IDbLogger _dbLogger;

        public RegisterController(UseCaseHandler handler, IRegisterUserCommand command, IDbLogger dbLogger)
        {
            _handler = handler;
            _command = command;
            _dbLogger = dbLogger;
        }

        /// <summary>
        /// Register User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/register
        ///     {
        ///         "Email" : "veljko.vulovic.282.18@ict.edu.rs",
        ///         "Username" : "velja",
        ///         "FirstName" : "Veljko",
        ///         "LastName" : "Vulovic",
        ///         "Password" : "Testiranje1!"
        ///     }
        ///
        /// </remarks>
        /// <returns>Successfully created user message</returns>
        /// <response code="201">Returns the successfully created message</response>
        /// <response code="422">Returns the unprocessable entity error</response>
        /// <response code="500">Returns Server error</response>   
        [HttpPost]
        public IActionResult Post([FromBody] RegisterDto dto)
        {
            try
            {
                _handler.HandleCommand(_command, dto);
                _dbLogger.Log("Register", "Email: "+dto.Email);

                return StatusCode(StatusCodes.Status201Created, "Successfully registered new user with email: "+dto.Email);
            }
            catch(ValidationException ex)
            {

                return ex.Errors.AsUnprocessableEntity();
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error creating new user", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error registering new user: " + ex.Message);
            }
        }
    }
}
