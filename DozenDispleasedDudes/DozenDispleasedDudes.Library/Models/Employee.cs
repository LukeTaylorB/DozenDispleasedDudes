using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace DozenDispleasedDudes.Library.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Id { get; set; }

        

       
        public override string ToString()
        {
            return $"{Id}, {Name} ";
        }
    }

}
