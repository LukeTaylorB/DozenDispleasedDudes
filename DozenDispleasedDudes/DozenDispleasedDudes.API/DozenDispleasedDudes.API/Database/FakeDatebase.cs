using DozenDispleasedDudes.Models;

namespace DozenDispleasedDudes.API.Database
{
    public class FakeDatebase
    {
        //public List<Client> Roster { get { return _roster; } }
        public static List<Client> Clients = new List<Client> 
        {
            new Client
            { 
                Id = 1, Name = "ExampleClient", 
                OpenDate = new DateTime(),
                ClosedDate = new DateTime(),
                IsActive = false,
                Notes = string.Empty
            }
        };
        public static int LastClientId
            => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
    }
}
