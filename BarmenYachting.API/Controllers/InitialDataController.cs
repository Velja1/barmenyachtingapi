using BarmenYachting.Application.Logging;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarmenYachting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {

        private BarmenYachtingDbContext _context;
        private IDbLogger _dbLogger;

        public InitialDataController(IDbLogger dbLogger, BarmenYachtingDbContext context)
        {
            _dbLogger = dbLogger;
            _context = context;
        }
        [HttpPost]
        public IActionResult Post()
        {
            try {

                if (_context.Manufacters.Any() ||
                    _context.Polls.Any() ||
                    _context.Roles.Any() ||
                    _context.Types.Any() ||
                    _context.Users.Any() ||
                    _context.Vessels.Any())
                {
                    return Conflict("Initial data already created");
                }

                var manufacters = new List<Manufacter>
                {
                    new Manufacter {Name="Monterey"},
                    new Manufacter {Name="Cranchi"},
                    new Manufacter {Name="Carver"},
                    new Manufacter {Name="Regal"},
                    new Manufacter {Name="Sea Ray"},
                    new Manufacter {Name="Tracker"}
                };

                var types = new List<Domain.Type>
                {
                    new Domain.Type {Name="Yacht"},
                    new Domain.Type {Name="Speedboat"}
                };

                var roles = new List<Role>
                {
                    new Role {Name="admin"},
                    new Role {Name="user"}
                };

                var users = new List<User>
                {
                    new User {
                        FirstName="Michael",
                        LastName="Connors",
                        Email="michael@gmail.com",
                        Username="michael123",
                        Password="sifra1",
                        Role=roles.First()
                    },
                    new User {
                        FirstName="Nicole",
                        LastName="Peters",
                        Email="nicole@gmail.com",
                        Username="nicole92",
                        Password="sifra2",
                        Role=roles.ElementAt(1)
                    }
                };

                var vessels = new List<Vessel>
                {
                    new Vessel
                    {
                        Model="295",
                        Price=138970,
                        Width=2.79,
                        Height=3.20,
                        Length=9.05,
                        Manufacter=manufacters.First(),
                        Type=types.First()
                    },

                    new Vessel
                    {
                        Model="C32",
                        Price=92100,
                        Width=3.79,
                        Height=2.20,
                        Length=10.05,
                        Manufacter=manufacters.ElementAt(1),
                        Type=types.First()
                    },

                    new Vessel
                    {
                        Model="A21",
                        Price=30090,
                        Width=4.79,
                        Height=1.20,
                        Length=5.05,
                        Manufacter=manufacters.ElementAt(2),
                        Type=types.ElementAt(1)
                    },

                    new Vessel
                    {
                        Model="M55",
                        Price=12100,
                        Width=2.39,
                        Height=2.01,
                        Length=4.10,
                        Manufacter=manufacters.ElementAt(3),
                        Type=types.First()
                    },

                    new Vessel
                    {
                        Model="Endurace",
                        Price=21200,
                        Width=3.95,
                        Height=2.14,
                        Length=4.15,
                        Manufacter=manufacters.ElementAt(4),
                        Type=types.ElementAt(1)
                    },

                    new Vessel
                    {
                        Model="A10",
                        Price=15090,
                        Width=3.77,
                        Height=2.00,
                        Length=9.11,
                        Manufacter=manufacters.ElementAt(5),
                        Type=types.First()
                    }
                };

                var polls = new List<Poll>
                {
                    new Poll
                    {
                        Rating=1,
                        Description="Bad vessel",
                        User=users.First(),
                        Vessel=vessels.First()
                    },new Poll
                    {
                        Rating=2,
                        Description="Good vessel",
                        User=users.ElementAt(1),
                        Vessel=vessels.ElementAt(3),
                    },
                    new Poll
                    {
                        Rating=3,
                        Description="Satisfied with choise",
                        User=users.ElementAt(1),
                        Vessel=vessels.ElementAt(3)
                    },
                    new Poll
                    {
                        Rating=4,
                        Description="Excelent vessel, very good",
                        User=users.ElementAt(1),
                        Vessel=vessels.ElementAt(4)
                    },
                    new Poll
                    {
                        Rating=5,
                        Description="Perfect",
                        User=users.ElementAt(1),
                        Vessel=vessels.ElementAt(5)
                    }
                };

                _context.Manufacters.AddRange(manufacters);
                _context.Types.AddRange(types);
                _context.Roles.AddRange(roles);
                _context.Users.AddRange(users);
                _context.Vessels.AddRange(vessels);
                _context.Polls.AddRange(polls);

                _context.SaveChanges();

                _dbLogger.Log("Create initial data", "");
                return StatusCode(StatusCodes.Status201Created, "Successfully created initial data");
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error creating initial data", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating initial data: "+ex.Message);
            }
        }
    }
}
