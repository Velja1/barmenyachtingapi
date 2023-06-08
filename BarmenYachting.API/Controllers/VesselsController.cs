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



        /// <summary>
        /// Get Vessels.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/vessels/?keyword=m55&perPage=5&page=1

        ///
        /// </remarks>
        /// <param name="keyword"></param>
        /// <param name="&perpage"></param>
        /// <param name="&page"></param>
        /// <returns>A paged query response with objects</returns>
        /// <response code="200">Returns the vessels query</response>
        /// <response code="500">Returns Server error</response>      
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetVesselsQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, search));

            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error getting vessels", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting vessels: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a Vessel.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/vessels/11
        ///
        /// </remarks>
        /// <returns>A vessel object</returns>
        /// <response code="200">Returns a vessel</response>
        /// <response code="500">Returns Server error</response>    
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromServices] IGetVesselQuery query)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, id));

            }
            catch(Exception ex)
            {
                _dbLogger.Log("Error getting vessel", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting vessel: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Vessel.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/vessel
        ///     {
        ///         "model":"Test",
        ///         "price":1,
        ///         "width":1,
        ///         "height":"1",
        ///         "length":1,
        ///         "manufacterId":1,
        ///         "typeId":1
        //      }
        ///
        /// </remarks>
        /// <returns>Successfully created vessel message</returns>
        /// <response code="201">Returns the successfully created message</response>
        /// <response code="422">Returns the unprocessable entity error</response>
        /// <response code="500">Returns Server error</response>   
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

        /// <summary>
        /// Deletes a Vessel.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/vessel/11
        ///
        /// </remarks>
        /// <returns>Successfully deleted vessel message</returns>
        /// <response code="200">Returns the deleted message</response>
        /// <response code="404">Returns the entity not found error</response>
        /// <response code="500">Returns Server error</response>   
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
