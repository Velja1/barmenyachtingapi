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

        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetManufactersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

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
