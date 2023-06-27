//using DozenDispleasedDudes.Library.Services;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//as of right now this contains both viewModel and ViewViewModel
//weird id is caused by forcing client not to be null 

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class ClientMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _clientMenuVisible;
        private bool _createClientFormVisible;
        private bool _clientRosterListVisible;
        private bool _updateClientFormVisible;
        private ClientService clientService;
        public bool _searchClientFormVisible;

        public bool ClientMenuVisible
        {
            get => _clientMenuVisible;
            set
            {
                if (_clientMenuVisible != value)
                {
                    _clientMenuVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool CreateClientFormVisible
        {
            get => _createClientFormVisible;
            set
            {
                if (_createClientFormVisible != value)
                {
                    _createClientFormVisible = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public bool ClientRosterListVisible
        {
            get => _clientRosterListVisible;
            set
            {
                if (_clientRosterListVisible != value)
                {
                    _clientRosterListVisible = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public bool UpdateClientFormVisible
        {
            get => _updateClientFormVisible;
            set
            {
                if (_updateClientFormVisible != value)
                {
                    _updateClientFormVisible = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public bool SearchClientFormVisible
        {
            get => _searchClientFormVisible;
            set
            {
                if (_searchClientFormVisible != value)
                {
                    _searchClientFormVisible = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public void ToggleUpdateClientForm()
        {
            UpdateClientFormVisible = !UpdateClientFormVisible;

        }

        public void ToggleCreateClientForm()
        {
            CreateClientFormVisible = !CreateClientFormVisible;
        }
        public void ToggleClientMenu()
        {
            ClientMenuVisible = !ClientMenuVisible;
        }
        public void ToggleRosterList()
        {
            ClientRosterListVisible = !ClientRosterListVisible;
        }
        public void ToggleSearchClientForm()
        {
            SearchClientFormVisible = !SearchClientFormVisible;
        }
        public bool rosterNotEmpty
        {
            get
            {
                return Roster.Count < 0;
            }
        }

        public ClientMenuViewModel()
        {
            client = new Client();
            clientService = ClientService.Current;
            //clientService.Add(client);
        }
        private Client? pineapple;
        public Client? GetClient()
        {
            return clientService.Get(client.Id);
        }

        public Client? client
        {
            get
            {
                return pineapple;
            }

            set
            {
                if (pineapple != value)
                {
                    if (value != null) 
                    {
                        pineapple = value;
                    }
                    


                }
            }
        }
        private string query;
        //Search Fixed by this 
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(Roster));
            }
        }

        public ObservableCollection<Client> Roster
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Roster);
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
            }
        }


        public string Name
        {
            get => client?.Name ?? string.Empty;

            set
            {
                if (client != null)
                {
                    client.Name = value;
                }

            }
        }
        public string Notes
        {
            get
            {
                return client?.Notes ?? string.Empty;
            }
            set { if (client != null) client.Notes = value; }
            //* proably why this doesnt work too ? 
            //get => client?.Name ?? string.Empty;
            //set { if (client != null) client.Name = value; }
        }
        public int Id { get; }
        public DateTime openDate
        {
            get
            {
                if (client != null)
                {
                    return client.OpenDate;
                }
                return DateTime.Now;
            }
            set { if (client != null) client.OpenDate = value; }
        }
        public DateTime? closeDate
        {
            get
            {
                if (client == null) return DateTime.Now;
                return client.ClosedDate;
            }
            set
            {
                if (client != null) { client.ClosedDate = value; }

            }
        }
        public bool isActive
        {
            get
            {
                if (client != null) return client.IsActive;
                else return false;
                //return client.IsActive;
            }
            set
            {
                if (client != null)
                {
                    client.IsActive = value;

                }
            }
        }
        public void Create()
        {
            if (client == null)
            {
                return;
            }

            var addClient = new Client(); //fixes update bug 
            addClient.Name = client.Name;
            addClient.OpenDate = client.OpenDate;
            addClient.ClosedDate = client.ClosedDate;
            addClient.IsActive = client.IsActive;
            addClient.Notes = client.Notes;
            clientService.Add(addClient);
            client = new Client();
            NotifyPropertyChanged("Roster"); //Is this correct way to Notify
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Notes");
            NotifyPropertyChanged("openDate");
            NotifyPropertyChanged("closeDate");
            NotifyPropertyChanged("isActive");
            ToggleCreateClientForm();
        }
        public void Delete()
        {
            if (client == null)
            {
                return;
            }
            ClientService.Current.Delete(client);
            client = new Client();
            NotifyPropertyChanged("Roster");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Notes");
            NotifyPropertyChanged("openDate");
            NotifyPropertyChanged("closeDate");
            NotifyPropertyChanged("isActive");

        }
        public void UpdateForm()
        {
            if (client == null)
            {
                return;
            }
            var check = client;
            NotifyPropertyChanged("Roster"); //Is this correct way to Notify
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Notes");
            NotifyPropertyChanged("openDate");
            NotifyPropertyChanged("closeDate");
            NotifyPropertyChanged("isActive");
            ToggleUpdateClientForm();
        }
        public void Update()
        {
            if (client == null)
            {
                return;
            }

            // Update the client properties
            client.Name = Name;
            client.Notes = Notes;
            client.OpenDate = openDate;
            client.ClosedDate = closeDate;
            client.IsActive = isActive;
            client = new Client();
            NotifyPropertyChanged("Roster");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Notes");
            NotifyPropertyChanged("openDate");
            NotifyPropertyChanged("closeDate");
            NotifyPropertyChanged("isActive");
            ToggleUpdateClientForm();
        }
    }
}