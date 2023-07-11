using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.Models;
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
    }
}
