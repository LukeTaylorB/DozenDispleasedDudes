using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Models
{
    public class Client
    {
       // private static int lastId = 0; 
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        /// <summary>
        /// The probelem is because update Loads in the data of selecteed student in the form to change it
        /// </summary>
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"{Id}, {Name}, {OpenDate}, {ClosedDate}, {Notes}";
        }
        public string shortString()
        {
            return $"Client [Id: {Id}, Name: {Name}]";
        }


        /*
         * public string shortString()
        {
            return $" {Id} {Name}";
        }
        public Client()
        {
            Id = ++lastId;
            Name = string.Empty;
            OpenDate = new DateTime();
            ClosedDate = new DateTime();
            IsActive = false;
            Notes = string.Empty;
        }
        public Client(string name, DateTime openDate, bool isActive, DateTime? closeDate, string notes)
        {
            Id = ++lastId;
            Name = name;
            OpenDate = openDate;
            ClosedDate = closeDate;
            IsActive = isActive;
            Notes = notes;
        }
        */

    }
}
