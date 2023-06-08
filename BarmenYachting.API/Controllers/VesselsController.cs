using BarmenYachting.Application.Logging;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;
using BarmenYachting.Implementation;
using BarmenYachting.Application.UseCases.DTO.Searches;
using BarmenYachting.Application.UseCases.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BarmenYachting.Application.UseCases.Commands;
using FluentValidation;
using BarmenYachting.Api.Extensions;
using BarmenYachting.Application.DTO;
using BarmenYachting.Application.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarmenYachting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VesselsController : ControllerBase
    {

        private UseCaseHandler _handler;
        private IDbLogger _dbLogger;

        public VesselsController(IDbLogger dbLogger, UseCaseHandler handler)
        {
            _dbLogger = dbLogger;
            _handler = handler;
        }

        // GET: api/<VesselsController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetVesselsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<VesselsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromServices] IGetVesselQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<VesselsController>
        [HttpPost("/api/addVessel")]
        public IActionResult CreateVessel([FromBody] CreateVesselDto dto, [FromServices] ICreateVesselCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                _dbLogger.Log(command.Name, "Vessel: " + dto.Model);
                return StatusCode(StatusCodes.Status201Created, "Successfully created Vessel: " + dto.ManufacterId);
            }
            catch (ValidationException ex)
            {
                _dbLogger.Log("Error creating Vessel", "Vessel model: " + ex.Errors);
                return ex.Errors.AsUnprocessableEntity();
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error creating Vessel", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Vessel: " + ex.Message);
            }
        }

        // DELETE api/<VesselsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteVesselCommand command)
        {
            try
            {
                _handler.HandleCommand(command, id);
                _dbLogger.Log(command.Name, "ID: " + id);
                return Ok("Successfully deleted Vessel with id: " + id);
            }
            catch (EntityNotFoundException ex)
            {
                _dbLogger.Log("Error deleting Vessel", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error deleting Vessel", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Vessel: " + ex.Message);
            }
        }
    }
}
