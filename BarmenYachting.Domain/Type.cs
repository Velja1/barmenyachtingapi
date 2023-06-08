using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Domain
{
    public class Type : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Vessel> Vessels { get; set; } = new List<Vessel>();
    }
}
