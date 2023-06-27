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

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    
    public class ClientViewModel : INotifyPropertyChanged
    {
        //public bool ClientSave;
        public Client Model { get; set; }
        
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                //if this is a new client, we have no projects to return yet
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<ProjectViewModel>();
                }
                return new ObservableCollection<ProjectViewModel>(ProjectService
                    .Current.Portfolio.Where(p => p.ClientId == Model.Id)
                    .Select(r => new ProjectViewModel(r)));
            }
        }
        
       
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }
        public ICommand DeleteCommand { get; private set; }
        public ICommand DetailsCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectCommand { get; private set; }
        public ICommand ShowProjectsCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ExecuteDelete(int id)
        {
            ClientService.Current.Delete(id);
        }
        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientForm?clientId={id}");
        }
        public void ExecuteDetails(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetail?clientId={id}");
        }

        //-------------Important-------------------------------------------
        //Where implementation of Projects and Project Detail exclusivly used for time bieng
        public void ExecuteShowProjects(int id)
        {
            Shell.Current.GoToAsync($"//Projects?clientId={id}&clientSave={true}");//&clientSave={true}

        }
        public void ExecuteAddProject()
        {
            AddOrUpdate(); //save the client so that we have an id to link the project to
            //TODO: if we cancel the creation of this client, we need to delete it on cancel.
            Shell.Current.GoToAsync($"//ProjectForm?clientId={Model.Id}");
        }

        //-------------------------------------------------------------------------
        public void RefreshProjects()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        
        

        public void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));
            
            DetailsCommand = new Command(
                (c) => ExecuteDetails((c as ClientViewModel).Model.Id));
            AddProjectCommand = new Command(
                 (c) => ExecuteAddProject());
            ShowProjectsCommand = new Command(
                (c) => ExecuteShowProjects((c as ClientViewModel).Model.Id));
        }
        public ClientViewModel() 
        {
            Model = new Client();
            SetupCommands();
        }
        public ClientViewModel(int clientId)
        {
            if(clientId == 0)
            {
                Model = new Client();
            }
            else
            {
                Model = ClientService.Current.Get(clientId);
            }
            SetupCommands();
        }
        public ClientViewModel(Client client)
        {
            Model = client;
            SetupCommands();
        }
        public void AddOrUpdate()
        {
            var check = Model.IsActive;
            ClientService.Current.AddOrUpdate(Model);
        }
    }
}
