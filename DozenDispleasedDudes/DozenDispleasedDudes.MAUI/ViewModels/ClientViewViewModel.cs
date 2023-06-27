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

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class ClientViewViewModel : INotifyPropertyChanged
    {
        public Client SelectedClient { get; set; }
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
        public ObservableCollection<ClientViewModel> Roster
        {
            get
            {
                if (string.IsNullOrEmpty(query))
                {
                    return
                    new ObservableCollection<ClientViewModel>
                    (ClientService
                        .Current.Roster
                        .Select(c => new ClientViewModel(c)).ToList());
                }
                var fetched = new ObservableCollection<Client>(ClientService.Current.Search(Query));
                return new ObservableCollection<ClientViewModel>(fetched.Select(c=> new ClientViewModel(c)).ToList());

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Delete()
        {
            if (SelectedClient != null)
            {
                ClientService.Current.Delete(SelectedClient.Id);
                SelectedClient = null;
                NotifyPropertyChanged(nameof(Roster));
                NotifyPropertyChanged(nameof(SelectedClient));
            }
        }
        public void RefreshClientList()
        {
            NotifyPropertyChanged(nameof(Roster));
        }
        public bool ClientSelected()
        {
            if(SelectedClient == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
