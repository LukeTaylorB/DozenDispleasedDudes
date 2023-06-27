
using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Services
{
    public class ProjectService
    {

        private List<Project> _portfolio;
        private static ProjectService? _instance;
        private static object _lock = new object();

        public static ProjectService Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProjectService();
                    }
                }

                return _instance;
            }
        }

        private ProjectService()
        {
            _portfolio = new List<Project>();
        }

        public List<Project> Portfolio { get { return _portfolio; } }

        public List<Project> Search(string query)
        {
            return _portfolio.Where(p => p.ShortName.ToUpper().Contains(query.ToUpper()) || p.LongName.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public Project? Get(int id)
        {
            return _portfolio.FirstOrDefault(p => p.Id == id);
        }
        public bool AddOrUpdate(Project p)
        {
            if (p.Id == 0)
            {
                p.Id = LastId + 1;
                Portfolio.Add(p);
                return true;
            }
            else { return false; }
        }
        public void Add(Project project)
        {
            if (project != null)
            {
                Portfolio.Add(project);
            }
            
        }

        public void Delete(int id)
        {
            var projectToRemove = Get(id);
            if (projectToRemove != null)
            {
                _portfolio.Remove(projectToRemove);
            }
        }
        private int LastId
        {
            get
            {
                return Portfolio.Any() ? Portfolio.Select(c => c.Id).Max() : 0;
            }
        }
        public void Delete(Project p)
        {
            _portfolio.Remove(p);
        }
    













//Hold over from CLI
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

        public void Update(Project c)
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
                Console.WriteLine("S. Edit Notes for Project");
                Console.WriteLine("L. Update Project Attributes (Name)"); // current support only for name on actual attributes.
                Console.WriteLine("X. Leave This Menu ");
                var choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "short";
                }
                else if (choice.Equals("L", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "long";
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
        public string? UpdateAction(Project c, string s)
        {
            if (s.Equals("short", StringComparison.InvariantCultureIgnoreCase))
            {
                var oldShort = c.ShortName;
                Console.WriteLine($"Current Short Name: ");
                Console.WriteLine($" {oldShort} ");
                Console.WriteLine("Type New Short Name and Hit Enter, If You Would Like to Keep it the Way it is Hit Enter Before Typing or Leave Input Empty");
                var updates = Console.ReadLine() ?? string.Empty;
                if (updates.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"Name Not Changed");
                    Console.WriteLine($"Name: {oldShort} ");
                    return oldShort;
                }
                else
                {

                    var isValid = false;
                    while (!isValid)
                    {
                        Console.WriteLine($"Project {c.Id} New Name will be {updates}");
                        Console.WriteLine("Confirm This Change (Y/N)");
                        var choice = Console.ReadLine() ?? string.Empty;
                        if (choice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Confirmed");
                            c.ShortName = updates;
                            return oldShort;
                        }
                        else if (choice.ToUpper().Contains("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Canceled");
                            return oldShort;
                        }

                    }

                }
                return "error";
            }
            else if (s.Equals("Long", StringComparison.InvariantCultureIgnoreCase))
            {
                var oldLong = c.LongName;
                Console.WriteLine($"Name(current)");
                Console.WriteLine($" {oldLong} ");
                Console.WriteLine("Type New Name and Hit Enter, If You Would Like to Keep it the Way it is Hit Enter Before Typing or Leave Input Empty");
                var updates = Console.ReadLine() ?? string.Empty;
                if (updates.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"Name Not Changed");
                    Console.WriteLine($"Name: {oldLong} ");
                    return oldLong;
                }
                else
                {

                    var isValid = false;
                    while (!isValid)
                    {
                        Console.WriteLine($"Project {c.Id} New Name will be {updates}");
                        Console.WriteLine("Confirm This Change (Y/N)");
                        var choice = Console.ReadLine() ?? string.Empty;
                        if (choice.ToUpper().Contains("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Confirmed");
                            c.LongName = updates;
                            return oldLong;
                        }
                        else if (choice.ToUpper().Contains("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValid = true;
                            Console.WriteLine("Action Canceled");
                            return oldLong;
                        }

                    }

                }
                return "error";
            }

            return "error";
        }
        
    }
}
