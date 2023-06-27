using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;

namespace DozenDispleasedDudes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool emptyRoster = true;
            bool emptyPortfolio = true;
            while (true)
            {

                Console.WriteLine("Welcome");
                Console.WriteLine("C. For Client Menu");
                Console.WriteLine("P. For Project Menu");
                var menuChoice = Console.ReadLine() ?? string.Empty;
                if (menuChoice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    //List<Client> roster = new List<Client>();

                    emptyRoster = ClientMenu(emptyRoster);
                }
                if (menuChoice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                {
                    //List<Project> portfolio = new List<Project>();
                    emptyPortfolio = ProjectMenu(emptyRoster,emptyPortfolio);
                }
               
                else
                {
                    break;
                }
            }
        }

        static bool ClientMenu(bool emptyRoster)
        {
            //
            if (emptyRoster == false)
            {
                var clientService = ClientService.Current;
            }
            while (true)
            {
                Console.WriteLine("C. Create a Client");
                Console.WriteLine("R. List Client Roster");
                Console.WriteLine("U. Update a Client Info");
                Console.WriteLine("D. Delete a Client");
                Console.WriteLine("Q. Quit");
                var choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    //ClientHelper clientEntry = new ClientHelper();
                    if (emptyRoster == true)
                    {
                        var clientService = ClientService.Current;
                        emptyRoster = false;
                        
                    }
                    else
                    {
                        ClientHelper clientEntry = new ClientHelper();
                        var client = clientEntry.ClientEntry();
                        ClientService.Current.Add(client);
                    }

                }
                else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyRoster == true)
                    {
                        Console.WriteLine("No Clients to Display. Your roster (list of Clients) is currently empty. Would you like to add a Client? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var clientService = ClientService.Current;
                            emptyRoster = false;
                        }
                    }
                    else
                    {
                        //Display Roster
                        //var clientService = ClientService.Current;
                        ClientService.Current.Read();
                    }
                }
                else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyRoster == true)
                    {
                        Console.WriteLine("No Clients to update would you like to add a Client? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var clientService = ClientService.Current;
                            emptyRoster = false;
                        }
                    }
                    else
                    {
                        //ClientHelper clientHelper = new ClientHelper();
                        var clientService = ClientService.Current;
                        bool isValid = false;



                        while (!isValid)
                        {
                            Console.WriteLine("Enter a Client Name( or Id you would like to Update. To see Client Roster(R)");
                            var userEntry = Console.ReadLine();
                            int clientId;
                            Client clientToUpdate = null;
                            if (int.TryParse(userEntry, out clientId))
                            {
                                clientToUpdate = clientService.Get(clientId);
                            }
                            else
                            {
                                clientToUpdate = clientService.Get();//userEntry);
                            }
                            if (userEntry.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ClientService.Current.Read();
                            }
                            else if (clientToUpdate != null)
                            {

                                isValid = true;
                                //Console.WriteLine($"{clientToUpdate.shortString()} was updated");
                                clientService.Update(clientToUpdate);
                            }
                            else
                            {
                                Console.WriteLine("Client you entered was not valid. Would you like to try again? (Y)es");
                                var reChoice = Console.ReadLine() ?? string.Empty;
                                var wantsReattempt = reChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase);
                                if (!wantsReattempt)
                                {
                                    isValid = true;
                                    Console.WriteLine("A valid Client from the roster was not given. No Clients have been updated the Roster.");
                                }
                            }
                        }

                    }//end of non empty roster logic (update function)
                }
                else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyRoster == true)
                    {
                        Console.WriteLine("No Clients to delete would you like to add a Client? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var clientService = ClientService.Current;
                            emptyRoster = false;
                        }
                    }
                    else
                    {
                        //ClientHelper clientHelper = new ClientHelper();
                        var clientService = ClientService.Current;
                        bool isValid = false;

                        
                        while (!isValid)
                        {
                            Console.WriteLine("Enter a Client name or Id you would like to delete. To see Client Roster(R)");
                            var userEntry = Console.ReadLine();
                            int clientId;
                            Client clientToRemove = null;
                            if (int.TryParse(userEntry, out clientId))
                            {
                                clientToRemove = clientService.Get(clientId);
                            }
                            else
                            {
                                clientToRemove = clientService.Get(userEntry);
                            }
                            if (userEntry.ToUpper().Equals("R", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ClientService.Current.Read();
                            }
                            else if (clientToRemove != null)
                            {

                                Console.WriteLine($"{clientToRemove.shortString()} was deleted");
                                clientService.Delete(clientToRemove);
                                isValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Client you entered was not valid. Would you like to try again? (Y)es");
                                var reChoice = Console.ReadLine() ?? string.Empty;
                                var wantsReattempt = reChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase);
                                if (!wantsReattempt)
                                {
                                    isValid = true;
                                    Console.WriteLine("A valid Client from the roster was not given. No Clients have been removed from the Roster.");
                                }
                            }
                        }

                    }//end of non empty roster logic (delete function)
                }//end of delete
                else
                {
                    //returns emptyRoster flag when exiting menu to save for potential reentrance
                    return emptyRoster;
                }
            }//end of client menu loop
        }//end of client menu function

        static bool ProjectMenu(bool emptyRoster,bool emptyPortfolio)
        {
            if (emptyRoster)
            {
                Console.WriteLine("There Are Curently No Clients. Unfortunatly that Means There are no Projects in The Portfolio and None can be Created. ");
                Console.WriteLine("You can However Create a Client in the Client Menu. Which you can access by hitting C. In the menu you were in previously, Which you will be defaulted to after this Message");
                return false;
            }
            while (true)
            {
                Console.WriteLine("C. Create a Project");
                Console.WriteLine("R. List Projects");
                Console.WriteLine("U. Update a Project");
                Console.WriteLine("D. Delete a Project");
                Console.WriteLine("Q. Quit");

                var choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (choice.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //ClientHelper clientEntry = new ClientHelper();
                        ProjectHelper projectHelper = new ProjectHelper();
                        var clientService = ClientService.Current;
                        var projectsClient = projectHelper.ProjectEntry();
                        if (emptyPortfolio == true)
                        {

                            var projectService = ProjectService.Current;
                            emptyPortfolio = false;
                            //var projectService = ProjectService.Current;
                        }
                        else
                        {
                            ProjectHelper projectEntry = new ProjectHelper();
                            var project = projectEntry.ProjectEntry();
                            ProjectService.Current.Add(project);
                        }

                    }
                }
                else if (choice.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyPortfolio == true)
                    {
                        Console.WriteLine("No Projects to Display. Your Portfolio (list of CProjects) is currently empty. Would you like to add a Project? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var projectService = ProjectService.Current;
                            emptyPortfolio = false;
                        }
                    }
                    else
                    {
                        //ProjectService.Current.Read();
                    }
                }
                else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyPortfolio == true)
                    {
                        Console.WriteLine("No Projects to Display. Your Portfolio (list of CProjects) is currently empty. Would you like to add a Project? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var projectService = ProjectService.Current;
                            emptyPortfolio = false;
                        }
                    }
                    else
                    {
                        var projectService = ProjectService.Current;
                        bool isValid = false;



                        while (!isValid)
                        {
                            Console.WriteLine("Enter A Project Name or id you would like to delete. (R) to List projects");
                            var userEntry = Console.ReadLine();
                            int projectId;
                            Project projectToUpdate = null;
                            if (int.TryParse(userEntry, out projectId))
                            {
                                projectToUpdate = projectService.Get(projectId);
                            }
                            else
                            {
                                //projectToUpdate = projectService.Get(userEntry);
                            }
                            if (userEntry.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                            {
                                //ProjectService.Current.Read();
                            }
                            else if (projectToUpdate != null)
                            {

                                isValid = true;
                                projectService.Update(projectToUpdate);
                            }
                            else
                            {
                                Console.WriteLine("Client you entered was not valid. Would you like to try again? (Y)es");
                                var reChoice = Console.ReadLine() ?? string.Empty;
                                var wantsReattempt = reChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase);
                                if (!wantsReattempt)
                                {
                                    isValid = true;
                                    Console.WriteLine("A valid Client from the roster was not given. No Clients have been updated the Roster.");
                                }
                            }
                        }


                    }
                }
                else if (choice.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (emptyPortfolio == true)
                    {
                        Console.WriteLine("No Projects to Display. Your Portfolio (list of CProjects) is currently empty. Would you like to add a Project? (Y)es  ");
                        var ReChoice = Console.ReadLine() ?? string.Empty;
                        if (ReChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var projectService = ProjectService.Current;
                            emptyPortfolio = false;
                        }
                    }
                    else
                    {
                        var projectService = ProjectService.Current;
                        bool isValid = false;

                        
                        while (!isValid)
                        {
                            Console.WriteLine("Enter a Client name or Id you would like to delete. To see Client Roster(R)");

                            var userEntry = Console.ReadLine();
                            int projectId;
                            Project projectToRemove = null;
                            if (int.TryParse(userEntry, out projectId))
                            {
                                projectToRemove = projectService.Get(projectId);
                            }
                            else
                            {
                                //projectToRemove = projectService.Get(userEntry);
                            }
                            if (userEntry.Equals("R", StringComparison.InvariantCultureIgnoreCase))
                            {
                                //ProjectService.Current.Read();
                            }
                            else if (projectToRemove != null)
                            {
                                isValid = true;
                                //Console.WriteLine($"{clientToRemove.shortString()} was deleted");
                                projectService.Delete(projectToRemove);
                            }
                            else
                            {
                                Console.WriteLine("Client you entered was not valid. Would you like to try again? (Y)es");
                                var reChoice = Console.ReadLine() ?? string.Empty;
                                var wantsReattempt = reChoice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase);
                                if (!wantsReattempt)
                                {
                                    isValid = true;
                                    Console.WriteLine("A valid Project from the Portfolio was not given. No Projects have been removed from the Portfolio.");
                                }
                            }
                        }

                    }//end of non empty roster logic (delete function)
                }
            }
        }//end of ProjectMenu function 

    }//end of Program(class)

}//end of namespace