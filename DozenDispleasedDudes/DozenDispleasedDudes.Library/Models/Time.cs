using DozenDispleasedDudes.Services;
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

        public decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal EmployeeRate { get; set; }
        //public decimal? Cost = EmployeeRate * Hours;
        public int? BillId { get; set; }
        public bool IsSelected { get; set; }
        public decimal Cost { get; set; }
        public Employee? Employee { get; set; }
       

        public override string ToString()
        {
            var proj = ProjectService.Current.Get(ProjectId);
            var projName = proj.ShortName;
            return $"\t EmployeeId: {EmployeeId} EmployeeRate: ${(EmployeeRate).ToString("F2")}/Hr TimerStart {start}  TimerStop {Stop}  Hours ( {(Hours).ToString("F3")} )";
        }
        public string BillString()
        {
            return $"Item: {ToString()} \t  Item Cost: ( Employee Rate ${EmployeeRate} X  {(Hours).ToString("F3")} Hours ) = ${(Cost).ToString("F2")}";
        }
    }
}
