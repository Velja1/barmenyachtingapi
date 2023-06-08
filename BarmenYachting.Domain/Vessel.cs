using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Domain
{
    public class Vessel : Entity
    {
        public string Model { get; set; }
        public decimal Price { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public int ManufacterId { get; set; }
        public int TypeId { get; set; }

        public virtual Manufacter Manufacter { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();
    }
}
