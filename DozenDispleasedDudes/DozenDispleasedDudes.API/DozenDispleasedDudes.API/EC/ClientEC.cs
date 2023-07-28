using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.Library;
using DozenDispleasedDudes.Library.DTO;
using DozenDispleasedDudes.Models;

namespace DozenDispleasedDudes.API.EC
{
    public class ClientEC
    {
        //when passing to DATAbase switch from dto.
        public ClientDTO AddOrUpdate(ClientDTO client)
        {
            ClientDTO dto = new ClientDTO();
            if (client.Id > 0)
            {
                /*
                var clientToUpdate
                    = FakeDatebase.Clients
                    .FirstOrDefault(c => c.Id == client.Id);
                if (clientToUpdate != null)
                {
                    FakeDatebase.Clients.Remove(clientToUpdate);
                }
                */
                dto = new ClientDTO(MsSqlContext.Current.UpdateClient(new Client(client))); //jank dispalys doesnt save
            }
            else
            {
                //lient.Id = FakeDatebase.LastClientId + 1;
                
                 dto =(new ClientDTO( MsSqlContext.Current.InsertClient(new Client(client))));
            }

            return dto;
        }
        public ClientDTO? Get(int id)
        {
            var returnValue = MsSqlContext.Current.GetClients().FirstOrDefault(c => c.Id == id) ?? new Client();
            return new ClientDTO(returnValue);
        }
        public ClientDTO? Delete(int id)
        {
            var clientToDelete = MsSqlContext.Current.GetClients().FirstOrDefault(c => c.Id == id);
            if (clientToDelete != null)
            {
                //FakeDatebase.Clients.Remove(clientToDelete);
                MsSqlContext.Current.DeleteClient(clientToDelete.Id);
            }
            return clientToDelete != null ? new ClientDTO(clientToDelete):null;
        }
        public IEnumerable<ClientDTO> Search(string query = "")
        {
            var test = MsSqlContext.Current.GetClients();
            return test.
                Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()))
                .Take(1000)
                .Select(c=>new ClientDTO(c));
        }
    }

}
