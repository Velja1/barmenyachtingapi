using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.UseCases.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; } //Required, Regex, Unique
        public string Username { get; set; } //Required, Min|Max, Regex, Unique
        public string FirstName { get; set; } //Required, Regex
        public string LastName { get; set; } //Required, Regex
        public string Password { get; set; } //Required, min|max, Regex
    }
}
