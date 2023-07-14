
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DozenDispleasedDudes.Services
{
    public class ClientService
    {

        //ClientHelper initEntry = new ClientHelper();
        private List<Client> _roster;
        public List<Client> Roster { get { return _roster;  } }
        private static ClientService? instance;
        private static object _lock = new object();
        public static ClientService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();//how to make this work with a parameter
                    }
                }

                return instance;
            }
        }
        //How to create without a pre made list
        private ClientService()
        {
            var response = new WebRequestHandler().Get("/Client").Result;
            _roster = JsonConvert.DeserializeObject<List<Client>>(response) ?? new List<Client>();
            /*
             * pre client switch constructor
            _roster = new List<Client>
            {
                 new Client{ Id = 1, Name = "ExampleClient", OpenDate = new DateTime(),ClosedDate = new DateTime(),IsActive = false, Notes = string.Empty }
            };
            */
            //roster.Add(initEntry.ClientEntry());
        }
        

        public List<Client> Search(string query)
        {
            /*
            var response
               = new WebRequestHandler().Post("/search", query).Result;

            var q = JsonConvert.DeserializeObject<Client>(response);
            //return q;
            */
            return Roster.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }
        //public List<Client> Roster { get { return _roster; } }
        public Client? Get(int id)
        {
            /*var response = new WebRequestHandler()
                   .Get($"/Client/GetClients/{id}")
                   .Result;
           var client = JsonConvert.DeserializeObject<Client>(response);*/
            return _roster.FirstOrDefault(r => r.Id == id);
        }
        
        public void AddOrUpdate(Client c)
        {
             var response 
                = new WebRequestHandler().Post("/Client", c).Result;
            
            var myUpdatedClient = JsonConvert.DeserializeObject<Client>(response);
            if(myUpdatedClient != null)
            {
                var existingClient = _roster.FirstOrDefault(c => c.Id == myUpdatedClient.Id);
                if(existingClient == null)
                {
                    _roster.Add(myUpdatedClient);
                }else
                {
                    var index = _roster.IndexOf(existingClient);
                    _roster.RemoveAt(index);
                    _roster.Insert(index, myUpdatedClient);
                }
            }
           
        }

        public void Add(Client? client)
        {
            if (client != null)
            {
                _roster.Add(client);
            }
        }
        public void Read()
        {
            _roster.ForEach(Console.WriteLine);
        }
        private int LastId
        {
            get
            {
                return Roster.Any() ? Roster.Select(c => c.Id).Max() : 0;
            }
        }
        public void Delete(int id)
        {
            var response
                = new WebRequestHandler().Delete( $"/{id}").Result;

            var DelClient = JsonConvert.DeserializeObject<Client>(response);
            if (DelClient != null)
            {
                var existingClient = _roster.FirstOrDefault(c => c.Id == DelClient.Id);
                if (existingClient != null)
                {
                    var index = _roster.IndexOf(existingClient);
                    _roster.RemoveAt(index);
                }
            }
            /*
            var clientToRemove = Get(id);
            if (clientToRemove != null)
            {
                _roster.Remove(clientToRemove);
            }
            */
        }

        public void Delete(Client c)
        {
            var response
                = new WebRequestHandler().Delete( $"/{c.Id}").Result;

            var DelClient = JsonConvert.DeserializeObject<Client>(response);
            if (DelClient != null)
            {
                var existingClient = _roster.FirstOrDefault(c => c.Id == DelClient.Id);
                if (existingClient != null)
                {
                    var index = _roster.IndexOf(existingClient);
                    _roster.RemoveAt(index);
                }
             
               // _roster.Remove(c);
            }
        }


        //---------------------------------------------------------------------------------------------------------
        /*
        public Client? Get(string name)
        {
            List<Client> clients = new List<Client>(_roster.FindAll(n=> n.Name == name));
           Client client = null;
            if(clients.Count == 1)
            {
                return clients[0];
            }
            else if (clients.Count > 1)
            {
                clients.ForEach(Console.WriteLine);
                Console.WriteLine("There are multiple clients in the roster with that name. Please specify a id for the correct client");
                Console.WriteLine("Id: ");

                bool isValidId = false;
               

                while (!isValidId)
                {
                    Console.WriteLine("Id: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int id))
                    {
                        client = clients.Find(c => c.Id == id);

                        if (client != null)
                        {
                            isValidId = true;
                        }
                    }

                    if (!isValidId)
                    {
                        Console.WriteLine("Invalid ID. Please try again.");
                        Console.WriteLine("Id: ");
                    }
                }
                return client;
            }
            else
            {
                Console.WriteLine("That client is not in the roster");
                return client;
            }
             
        }
        */

        //for console app :( there are bugs because debuging certain problems require adjustments the class itself and all other functions
        //I didnt think it was wise to fix the console app evreytime something changed for proj 2 or 3


        public void Update(int id)
        {
            var c = Get(id);
            if (c != null)
            {
                var action = UpdatePrompt();
                var oldValue = UpdateAction(c, action);
                Console.WriteLine("Action finished"); ;
            }
        }

        public void Update(Client c)
        {
            if (c != null)
            {
                var action = UpdatePrompt();
                var oldValue = UpdateAction(c, action);
                Console.WriteLine("Action finished");

            }
        }
       
        public string UpdatePrompt() 
        {
            while (true)
            {
                Console.WriteLine("What Type of Changes Would You Like to Make");
                Console.WriteLine("E. Edit Notes for Client");
                Console.WriteLine("U. Update Client Attributes (Name)"); // current support only for name on actual attributes.
                Console.WriteLine("X. Leave This Menu ");
                var choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("E", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "edit";
                }
                else if (choice.Equals("U", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "update";
                }
                else if (choice.Equals("X", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "exit";
                }

                else
                {
                    Console.WriteLine("Unfortunatley the Selection You Made is Not Valid");
                }
            }
        }
        public string? UpdateAction(Client c, string s )
        {
            if(s.Equals("edit", StringComparison.InvariantCultureIgnoreCase))
            {
                var oldNotes = c.Notes;
                Console.WriteLine($"Notes(current)");
                Console.WriteLine($" {oldNotes} ");
                Console.WriteLine("Editing Notes, To Add Changes Type Below. To Finish Hit Enter, If This Was A Mistake Hit Enter and No Changes Will be Made! ");
                var updates = Console.ReadLine() ?? string.Empty;
                c.Notes = oldNotes + updates;
                return oldNotes;
            }
            else if (s.Equals("update", StringComparison.InvariantCultureIgnoreCase))
            {
                var oldName = c.Name;
                Console.WriteLine($"Name(current)");
                Console.WriteLine($" {oldName} ");
                Console.WriteLine("Type New Name and Hit Enter, If You Would Like to Keep it the Way it is Hit Enter Before Typing or Leave Input Empty");
                var updates = Console.ReadLine() ?? string.Empty;
                if (updates.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"Name Not Changed");
                    Console.WriteLine($"Name: {oldName} ");
                    return oldName;
                }
                else
                {
                    
                    var isValid = false;
                    while(!isValid)
                    {
                        Console.WriteLine($"Client {c.Id} New Name will be {updates}");
                        Console.WriteLine("Confirm This Change (Y/N)");
                        var choice = Console.ReadLine() ?? string.Empty;
                        if (choice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Confirmed");
                            c.Name = updates;
                            return oldName;
                        }
                        else if (choice.ToUpper().Contains("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Canceled");
                            return oldName;
                        }
                        
                    }
                    
                }
                return "error";  
            }

            return "error";   
        }

    }
}
