using BarmenYachting.Api.Extensions;
using BarmenYachting.Application.Emails;
using BarmenYachting.Application.Exceptions;
using BarmenYachting.Application.Logging;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.Application.UseCases.DTO.Searches;
using BarmenYachting.Application.UseCases.Queries;
using BarmenYachting.Implementation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BarmenYachting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactersController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IDbLogger _dbLogger;

        public ManufactersController(UseCaseHandler handler, IDbLogger dbLogger) {
            _handler = handler;
            _dbLogger = dbLogger;
        }

        /// <summary>
        /// Get Manufacters.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/manufacters/?keyword=mon&perpage=2&page=1

        ///
        /// </remarks>
        /// <param name="keyword"></param>
        /// <param name="&perpage"></param>
        /// <param name="&page"></param>
        /// <returns>A paged query response with objects</returns>
        /// <response code="200">Returns the manufacter query</response>
        /// <response code="500">Returns Server error</response>         
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetManufactersQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, search));
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error getting manufacter", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting manufacter: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Manufacter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/manufacters
        ///     {
        ///         "Name" : "ManufacterNew"
        ///     }
        ///
        /// </remarks>
        /// <returns>Successfully created manufacter message</returns>
        /// <response code="201">Returns the successfully created message</response>
        /// <response code="422">Returns the unprocessable entity error</response>
        /// <response code="500">Returns Server error</response>   
        [HttpPost]
        public IActionResult CreateManufacter([FromBody] CreateManufacterDto dto, [FromServices] ICreateManufacterCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                _dbLogger.Log(command.Name, "Name: " + dto.Name);
                return StatusCode(StatusCodes.Status201Created, "Successfully created manufacter: "+dto.Name);
            }
            catch (ValidationException ex)
            {
                _dbLogger.Log("Error creating manufacter", "Manufacter name: " + ex.Errors);
                return ex.Errors.AsUnprocessableEntity();
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error creating manufacter", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating manufacter: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a Manufacter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/manufacters/11
        ///
        /// </remarks>
        /// <returns>Successfully deleted manufacter message</returns>
        /// <response code="200">Returns the deleted message</response>
        /// <response code="404">Returns the entity not found error</response>
        /// <response code="500">Returns Server error</response>   
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteManufacterCommand command)
        {
            try
            {
                _handler.HandleCommand(command, id);
                _dbLogger.Log(command.Name, "ID: " + id);
                return Ok("Successfully deleted manufacter with id: " + id);
            }
            catch (EntityNotFoundException ex)
            {
                _dbLogger.Log("Error deleting manufacter", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error deleting manufacter", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting manufacter: " + ex.Message);
            }
        }
    }
}
