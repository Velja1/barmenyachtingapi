using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Domain
{
    public class Poll : Entity
    {
        public int Rating { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int VesselId { get; set; }

        public User User { get; set; }
        public Vessel Vessel { get; set; }
    }
}
