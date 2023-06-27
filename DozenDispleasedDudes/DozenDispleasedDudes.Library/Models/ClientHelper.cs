using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Models
{
    public class ClientHelper
    {public Client ClientEntry()
        {
            //Create stuff

            Console.WriteLine("Id: ");
            /*
            while(!int.TryParse(Console.ReadLine(), out int myInt)){
                Console.WriteLine("I know numbers can be hard for the english folks but I feel like a Lawyer should know better..... try again");
            }
            */
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("isActive (Y/N): ");
            var isActive = Console.ReadLine() ?? "N";

            Console.WriteLine("Open Date: ");
            var openDate = DateTime.Parse(Console.ReadLine());

            //end of if
            Console.WriteLine("Notes: ");
            var Notes = Console.ReadLine();
            bool iAct;
            var myClient = new Client
            {
                Id = id,
                Name = name ?? "Jeffery Redacted",
                OpenDate = openDate,
                Notes = Notes ?? "n/a"
            };

            if (isActive == "N")
            {
                iAct = false;
                Console.WriteLine("Close Date: ");
                var closeDate = DateTime.Parse(Console.ReadLine());
                myClient.ClosedDate = closeDate;
            }
            else
            {
                iAct = true;
                myClient.ClosedDate = null; // Set ClosedDate to null for active client
            }
            return myClient;
        }
        public string shortString(Client? Client)
        {
            //not needed anymore .... I think
            return $" {Client.Id} {Client.Name}";
        }
    }
        

}
