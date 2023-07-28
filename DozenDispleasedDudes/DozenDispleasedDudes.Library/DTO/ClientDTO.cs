using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }

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
        public DateTime DefaultDate = DateTime.Today;
        public ClientDTO()
        {
            Name = string.Empty;
            OpenDate = DefaultDate;
            ClosedDate = DefaultDate;
            IsActive = false;
            Notes = string.Empty;

        }
        public ClientDTO(Client c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
            this.OpenDate = c.OpenDate;
            this.ClosedDate = c.ClosedDate;
            this.IsActive = c.IsActive;
            this.Notes = c.Notes;
        }

       

       
    }
}
