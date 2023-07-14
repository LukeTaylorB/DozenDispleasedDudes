using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.API.Database;
using DozenDispleasedDudes.Models;

namespace DozenDispleasedDudes.API.EC
{
    public class ClientEC
    {
        public Client AddOrUpdate(Client client)
        {
            if (client.Id > 0)
            {
                var clientToUpdate
                    = FakeDatebase.Clients
                    .FirstOrDefault(c => c.Id == client.Id);
                if (clientToUpdate != null)
                {
                    FakeDatebase.Clients.Remove(clientToUpdate);
                }
                FakeDatebase.Clients.Add(client);
            }
            else
            {
                client.Id = FakeDatebase.LastClientId + 1;
                FakeDatebase.Clients.Add(client);
            }

            return client;
        }

        public IEnumerable<Client> Search(string query)
        {
            return FakeDatebase.Clients.
                Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper())).Take(1000);
        }
    }

}
