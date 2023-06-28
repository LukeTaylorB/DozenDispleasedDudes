using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Models
{
    public class Time
    {
        public DateTime? start { get; set; }
        public DateTime? Stop { get; set; }
        public string? Narrative { get; set; }

        public decimal? Hours { get; set; }
        public int ProjectId { get; set; }
        public int? EmployeeId { get; set; }

        public override string ToString()
        {
            return $"{ProjectId},{start}, {Hours}";
        }
    }
}
