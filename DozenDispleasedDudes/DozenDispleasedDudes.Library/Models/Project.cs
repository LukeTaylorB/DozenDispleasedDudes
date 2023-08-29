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
        private static int lastId = 0; // no longer used
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsActive { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int ClientId { get; set; }
        public bool IsSelected { get; set; }
        public Client? Client { get; set; }

        //private static List<Project> projects = new List<Project>();

        public string shortString()
        {
            return $"{Id}.  {LongName} ({ShortName}) : {Client.shortString()} "?? string.Empty;
        }
        public string Name()
        {
            return $" {LongName}\n";
        }
        public override string ToString()
        {
            if (IsActive == true)
            {
                return $"{Id}.  ({ShortName})  Status: Active (Opened {OpenDate})  "; //{LongName}
            }
            else
            {
                return $"{Id}.  ({ShortName}) Status: Closed ({ClosedDate})"; //{LongName}
            }
        }
        public string TimerString()
        {
            return $"{ShortName} \nClient{Client.Name}";
        }


    }
}
