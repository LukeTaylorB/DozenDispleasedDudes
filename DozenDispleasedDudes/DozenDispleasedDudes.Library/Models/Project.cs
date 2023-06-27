using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DozenDispleasedDudes.Models
{
    public class Project
    {
        private static int lastId = 0;
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsActive { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        //private static List<Project> projects = new List<Project>();

        public string shortString()
        {
            return $"{Id}.  {LongName} ({ShortName}) : {Client.shortString()} "?? string.Empty;
        }
        
        public override string ToString()
        {
            if (IsActive == true)
            {
                return $"{Id}. {LongName} ({ShortName})  Status: Active (Opened {OpenDate})  ";
            }
            else
            {
                return $"{Id}. {LongName} ({ShortName}) Status: Closed ({ClosedDate})";
            }
        }
        public string TimerString()
        {
            return $"{ShortName} \nClient{Client.Name}";
        }


    }
}
