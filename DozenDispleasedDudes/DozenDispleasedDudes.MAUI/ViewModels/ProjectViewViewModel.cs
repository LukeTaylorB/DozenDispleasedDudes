
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
    public class ProjectViewViewModel  : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Client SelectedClient { get; set; }
        public Project SelectedProject { get; set; }
        public static int clientViewOrigin;
        private string clientQuery;
        //Search Fixed by this 
        public ObservableCollection<Client> Roster
        {
            get
            {
                if (string.IsNullOrEmpty(ClientQuery))
                {
                    return new ObservableCollection<Client>(ClientService.Current.Roster);
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(ClientQuery));
            }
        }
        
        public string ClientQuery
        {
            get => clientQuery;
            set
            {
                clientQuery = value;
                NotifyPropertyChanged(nameof(Roster));
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
                NotifyPropertyChanged(nameof(Portfolio));
            }
        }
        public ObservableCollection<ProjectViewModel> Portfolio 
        {
            get
            {
                if (string.IsNullOrEmpty(query))
                {
                    return
                    new ObservableCollection<ProjectViewModel>
                    (Projects
                        .Select(c => new ProjectViewModel(c)).ToList());
                }
                var fetched = new ObservableCollection<Project>(ProjectService.Current.Search(Query));
                var matched = fetched.Where(obj => Projects.Contains(obj));
                return new ObservableCollection<ProjectViewModel>(matched.Select(c => new ProjectViewModel(c)).ToList());

            }
        }

        public Client Client { get; set; }
        public static bool ClientSave { get; set; }
        public ObservableCollection<Project> Projects
        {
           
            get
            {
                if (Client == null || Client.Id == 0 || ClientSave==false)
                {
                    return new ObservableCollection<Project>
                    (ProjectService.Current.Portfolio);
                }

                return new ObservableCollection<Project>
                    (ProjectService.Current.Portfolio
                    .Where(p => p.ClientId == Client.Id));
            }
        }
    
        
        public ProjectViewViewModel(int clientId,bool clientSave=false)
        {
            if (clientId > 0)
            {
                Client = ClientService.Current.Get(clientId);
                ClientSave = clientSave;
            }
            else
            {
                Client = new Client();
                //clientSave = false;
            }

        }
        public void RefreshProjectList()
        {
            NotifyPropertyChanged(nameof(Projects));
            NotifyPropertyChanged(nameof(Portfolio));
        }
      
    }

}
