using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.API.EC;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Library.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DozenDispleasedDudes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return FakeDatebase.Clients;
        }
        [HttpGet("/{id}")]
        public Client GetId(int id)
        {
            return FakeDatebase.Clients.FirstOrDefault(c => c.Id == id) ?? new Client();
        }

        [HttpDelete("/{id}")]
        public Client? Delete(int id)
        {
            var clientToDelete = FakeDatebase.Clients.FirstOrDefault(c => c.Id == id);
            if (clientToDelete != null)
            {
                FakeDatebase.Clients.Remove(clientToDelete);
            }
            return clientToDelete;
        }

        [HttpPost]
        public Client AddOrUpdate([FromBody] Client client)
        {
            return new ClientEC().AddOrUpdate(client);
        }
        
        [HttpPost("/search")]
        public IEnumerable<Client> Search([FromBody] QueryMessage query)
        {
            return new ClientEC().Search(query.Query);
        }
        
    }
}
