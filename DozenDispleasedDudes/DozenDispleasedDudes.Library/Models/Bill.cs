using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Models
{
    public class Bill
    {
        public Client client { get; set; }
        //public decimal TotalCost;
        
        //public List<decimal> ProjectCostList;
        //public List<decimal> TimeCostList; 
        //public string Description;
        //public DateTime InvoiceDate; 
        public List<Time> timeList { get; set; } //Grouped by Project ? 
        //public bool Active { get; set; }
        public int InvoiceId { get; set; }
        //public List<(Project p, int cost)> projAndCost { get;set; } 
        //public List<(Project p,List<(Time t, decimal TimeCost)>,decimal ProjectCost)> BreakTheMatrix;
        
        
        //there should be multiple touples here maybe even classes ex (proj/time) but If I can get it to print the data structured
        // im calling it a win since everything will be there and I need to speed it up.
        // I got timeList sent over perfectly based on selected. The xaml formatting is just ugly as hell
        // ive made no progress from there. I could Print just the InvoiceID and Cost in a List with a submit but 
        // I might as well just keep it on CDV if thats the case
        public decimal TotalCost;
    }
}
