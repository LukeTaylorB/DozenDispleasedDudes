using DozenDispleasedDudes.Library.DTO;
using DozenDispleasedDudes.Models;
using Microsoft.Data.SqlClient;
using System.Net;
using static Azure.Core.HttpHeader;

namespace DozenDispleasedDudes.API.Database
{
    public class MsSqlContext
    {
        private static MsSqlContext? instance;
        public static MsSqlContext Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new MsSqlContext();
                }
                return instance;
            }
        }
        private MsSqlContext()
        {
            connectionString = "Server=LPC;Database=DDD_DB;Trusted_Connection=True;TrustServerCertificate=True";
           
        }

        private string connectionString;
        //private static List<Client> clientsFromDB= new List<Client>();
        /* public List<Client> GetClients
        {
            get => clientsFromDB;
            set
            {
                var results = new List<Client>();
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = "select Id, Name, OpenDate, ClosedDate, Is  from Clients";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            results.Add(new Client
                            {
                                Id = (int)reader[0],
                                Name = reader[1]?.ToString() ?? string.Empty,
                                OpenDate = (DateTime)reader[2],
                                ClosedDate = (DateTime)reader[3],
                                IsActive = ((int)reader[4] == 1) ? true : false,
                                Notes = reader[5]?.ToString() ?? string.Empty,
                            });
                        }
                        conn.Close();
                    }
                }
                clientsFromDB = results;
                
            }
*/
        public List<Client> GetClients()
        {
                var results = new List<Client>();
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = "select Id, Name, OpenDate, ClosedDate, IsActive, Notes from Clients";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                        //(int)reader[4] == 1) ? true : false, (DateTime)reader[3]
                        var closedDate = reader.IsDBNull(3) ? (DateTime?)null : (DateTime)reader[3];
                        var test = new Client();

                        test.Id = (int)reader[0];
                        test.Name = reader[1]?.ToString() ?? string.Empty;
                        test.OpenDate = (DateTime)reader[2];
                        
                        test.ClosedDate = closedDate;
                       
                        test.IsActive = (bool)reader[4];
                        test.Notes = reader[5]?.ToString() ?? string.Empty;
                        



                        results.Add(test);
                        }
                        conn.Close();
                    }
                }
                return results;
            
        }
        /*
        private List<ClientDTO> clientDTOs;
        public List<ClientDTO> GetDTOs
        {
            get => clientDTOs;
            set
            {
                foreach (var client in clientsFromDB)
                {
                    ClientDTO clientDTO = new ClientDTO
                    {
                        Id = client.Id,
                        Name = client.Name,
                        OpenDate = client.OpenDate,
                        ClosedDate = client.ClosedDate,
                        IsActive = client.IsActive,
                        Notes = client.Notes
                    };

                    clientDTOs.Add(clientDTO);
                }
            }
        }
        */
        //change to procedures
        public Client InsertClient(Client newClient)
        {

            int result;
            using (var conn = new SqlConnection(connectionString))
            {
                //obvious use procedure once its set up
                var sql =$"InsertClient";
              
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var arrivedId = newClient.Id;
                    var errorCheck = (arrivedId == 0) ? true : false; // check Id = 0 before insert 
                    bool savedBool = newClient.IsActive;
                    int flag;
                    if (savedBool == true)
                    { flag = 1; }
                    else { flag = 0; }
                    //cmd.Parameters.Add(new SqlParameter("IsActive", flag));

                    cmd.Parameters.Add(new SqlParameter("Name", newClient.Name));
                    cmd.Parameters.Add(new SqlParameter("OpenDate", newClient.OpenDate));
                    cmd.Parameters.Add(new SqlParameter("ClosedDate", newClient.ClosedDate));
                    cmd.Parameters.Add(new SqlParameter("IsActive", flag));
                    cmd.Parameters.Add(new SqlParameter("Notes", newClient.Notes));

                    conn.Open();
                    result =(int)cmd.ExecuteScalar();
                    conn.Close();
                    newClient.Id  = result;
                    //newClient.IsActive = savedBool;
                    //var errorCheck2 = (arrivedId != result) ? true : false; // check If ID no longer 0 "result/current id != not org id" (somewhat unneccessary but identifies where occurs.
                }
            }
            return newClient;
        }
        public Client UpdateClient(Client updatedClient)
        {
            int result;
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = $"UpdateClient";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // error check 
                    var arrivedId = updatedClient.Id;
                    var errorCheck = (arrivedId > 0) ? true : false; // In right place if true 
                    bool savedBool = updatedClient.IsActive; 
                    int flag;
                    if (savedBool == true)
                    { flag = 1; }
                    else { flag = 0; }

                    cmd.Parameters.Add(new SqlParameter("Id", updatedClient.Id));

                    cmd.Parameters.Add(new SqlParameter("Name", updatedClient.Name));
                    cmd.Parameters.Add(new SqlParameter("OpenDate", updatedClient.OpenDate));
                    cmd.Parameters.Add(new SqlParameter("ClosedDate", updatedClient.ClosedDate));
                    cmd.Parameters.Add(new SqlParameter("IsActive", flag));
                    cmd.Parameters.Add(new SqlParameter("Notes", updatedClient.Notes));
                   
                    conn.Open();

                    // Execute the update query and get the number of rows affected
                    //result = (int)
                    cmd.ExecuteScalar();
                    conn.Close();
                    //updatedClient.Id = result;
                    //var errorCheck2 = (arrivedId == result) ? true : false; // Maintain Consistency on Id through update on DB check
                    //updatedClient.IsActive = savedBool;
                }
            }
            return updatedClient;
        }

        public void DeleteClient(int clientId)
        {
            //Would not neccesarlly need procedure here right since its int so sql Injection not posible 
            // need try catches

            int rowsAffected;
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = $"DELETE FROM Clients WHERE id = {clientId};";
                using (var cmd = new SqlCommand(sql, conn))
                {

                    conn.Open();

                    // Execute the delete query and get the number of rows affected
                    rowsAffected = (int)cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
         
        }

    }
}
