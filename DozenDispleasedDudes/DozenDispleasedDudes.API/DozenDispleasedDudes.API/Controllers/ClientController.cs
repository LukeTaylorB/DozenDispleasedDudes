using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.API.EC;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Library.Utilities;
using Microsoft.AspNetCore.Mvc;
using DozenDispleasedDudes.Library.DTO;

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
        public IEnumerable<ClientDTO> Get()
        {
            return new ClientEC().Search();
        }
        [HttpGet("/{id}")]
        public ClientDTO? GetId(int id)
        {
            return new ClientEC().Get(id);
        }

        [HttpDelete("/{id}")]
        public ClientDTO? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        [HttpPost]
        public ClientDTO AddOrUpdate([FromBody] ClientDTO dto)
        {
            return new ClientEC().AddOrUpdate(dto);
        }
        
        [HttpPost("/{query}")]
        public IEnumerable<ClientDTO> Search([FromBody] QueryMessage query)
        {
            return new ClientEC().Search(query.Query);
        }
        
    }
}
