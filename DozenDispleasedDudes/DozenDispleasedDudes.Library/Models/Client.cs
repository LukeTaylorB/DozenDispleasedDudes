﻿using DozenDispleasedDudes.Library.DTO;
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
        public Client()
        {
           
            Name = string.Empty;
            OpenDate = new DateTime();
            ClosedDate = new DateTime();
            IsActive = false;
            Notes = string.Empty;
        }
        public Client(ClientDTO dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.OpenDate = dto.OpenDate;
            this.ClosedDate = dto.ClosedDate;
            this.IsActive = dto.IsActive;
            this.Notes = dto.Notes;
        }

        /*
         * public string shortString()
        {
            return $" {Id} {Name}";
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
