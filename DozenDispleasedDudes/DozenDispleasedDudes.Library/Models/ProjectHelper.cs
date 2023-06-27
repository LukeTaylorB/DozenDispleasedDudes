using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Models
{
    public class ProjectHelper
    {
        public Project ProjectEntry()
        {
            //Console.WriteLine("What Client : ");
            var clientService = ClientService.Current;
            bool isValid = false;
            var myProject = new Project();
            

            while (!isValid)
            {
                Console.WriteLine("Enter a Client Name or Id that Should be Associated with the Project. To see Client Roster(R)");
                //add search functionality for non caps sensitive
                var userEntry = Console.ReadLine();
    //              var projectsClient = clientService.Get(userEntry);


                if (userEntry.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    ClientService.Current.Read();
                }
                else if (userEntry!= null)
                {

                    isValid = true;

                    //myProject.Client= projectsClient;

                    //myProject.ClientId = projectsClient.Id;

                    //Console.WriteLine($"{clientToUpdate.shortString()} was updated");
                    //clientService.Update(clientToUpdate);
                }
                else
                {
                    Console.WriteLine("Client you entered was not valid. Would you like to try again? (Y)es");
                    
                }
            }
                //var cId = int.Parse(Console.ReadLine() ?? "0");
                //var clientFromId = roster.FirstOrDefault(s => s.Id == cId);

                Console.WriteLine("Project Id: ");
            /*
            while(!int.TryParse(Console.ReadLine(), out int myInt)){
                Console.WriteLine("I know numbers can be hard for the english folks but I feel like a Lawyer should know better..... try again");
            }
            */
            var id = int.Parse(Console.ReadLine() ?? "0");


            Console.WriteLine("Short Name: ");
            var sName = Console.ReadLine();

            Console.WriteLine("Long Name: ");
            var lName = Console.ReadLine();

            Console.WriteLine("isActive (Y/N): ");
            var isActive = Console.ReadLine() ?? "N";

            Console.WriteLine("Open Date: ");
            var openDate = DateTime.Parse(Console.ReadLine() ?? "1/1/2000");


            //end of if
            

            bool iAct;


            myProject.Id = id;
            myProject.ShortName = sName;
            myProject.LongName = lName;
            myProject.OpenDate = openDate;
                
            

            if (isActive == "N")
            {
                iAct = false;
                Console.WriteLine("Close Date: ");
                var closeDate = DateTime.Parse(Console.ReadLine() ?? "1/2/2000");
                myProject.ClosedDate = closeDate;
            }
            else
            {
                iAct = true;
                myProject.ClosedDate = null; // Set ClosedDate to null for active project
            }

            myProject.IsActive = iAct;


            return myProject;
        }
    }
}
