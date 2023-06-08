using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Domain
{
    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime ExecutionTime { get; set; }
        public string Data { get; set; }
    }
}
