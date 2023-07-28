using DozenDispleasedDudes.Library.DTO;
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
    public class ProjectMenuViewModel : INotifyPropertyChanged
    {
        private bool _projectMenuVisible;
        private bool _createProjectFormVisible;
        private bool _projectListVisible;
        private bool _updateProjectFormVisible;
        private bool _searchProjectFormVisible;
        private ProjectService _projectService;
        private ClientService clientService; 
      
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProjectMenuViewModel()
        {
           ProjectMenuVisible = false;
            _projectService = ProjectService.Current;
            clientService = ClientService.Current;
            project = new Project();
            client = new Client();
            //NotifyPropertyChanged("Portfolio");
            //NotifyPropertyChanged("Roster");
        }
        public bool emptyRoster
        {
            get
            {
                return Roster.Count < 1;
            }
        }
        public bool emptyPortfolio
        {
            get
            {
                return Portfolio.Count < 1;
            }
        }
        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Portfolio");
        }

        public int Id
        {
            get { return project?.Id ?? 0; }
            set
            {
                if (project != null)
                {
                    project.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime OpenDate
        {
            get { return project?.OpenDate ?? DateTime.MinValue; }
            set
            {
                if (project != null)
                {
                    project.OpenDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? ClosedDate
        {
            get { return project?.ClosedDate; }
            set
            {
                if (project != null)
                {
                    project.ClosedDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsActive
        {
            get { return project?.IsActive ?? false; }
            set
            {
                if (project != null)
                {
                    project.IsActive = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public string ShortName
        {
            get 
            {
                //when going to main menu back from client thiss gets called but not for in xaml tf 
                //var check = project?.ShortName ?? "generic ex girlfriend name";
                //NotifyPropertyChanged("ShortName");
                return project?.ShortName ?? string.Empty; 
            }
            set
            {
                if (project != null)
                {
                    project.ShortName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LongName
        {
            get { return project?.LongName ?? string.Empty; }
            set
            {
                if (project != null)
                {
                    project.LongName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Client projectClient
        {
            get { return project.Client; }
            set
            {
                if (project != null)
                {
                    project.Client = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int ClientId
        {
            get { return project?.ClientId ?? 0; }
            set
            {
                if (project != null)
                {
                    project.ClientId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool InverseActive { get { return !IsActive; } }
        private string clientQuery;
        //Search Fixed by this 
        public string ClientQuery
        {
            get => clientQuery;
            set
            {
                clientQuery = value;
                NotifyPropertyChanged(nameof(Roster));
            }
        }
        public ObservableCollection<ClientDTO> Roster
        {
            get
            {
                if (string.IsNullOrEmpty(ClientQuery))
                {
                    return new ObservableCollection<ClientDTO>(ClientService.Current.Roster);
                }
                return new ObservableCollection<ClientDTO>(ClientService.Current.Search(ClientQuery));
            }
        }
        private Client? pineapple;
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
        public ObservableCollection<Project> Portfolio
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Project>(_projectService.Portfolio);
                }

                return new ObservableCollection<Project>(_projectService.Search(Query));
            }
        }
        public Project? GetProject()
        {
            return _projectService.Get(project.Id);
        }
        public ClientDTO? GetClient()
        {
            return clientService.Get(client.Id);
        }
        public Project? apple;
        public Project? project {
            get
            {
                return apple;
            }

            set
            {
                if (apple != value)
                {
                    if (value != null)
                    {
                        apple = value;
                    }
                }
            }
        }

        public void Create()
        {
            if (project == null)
            {
                return;
            }
            var addProject = new Project();
            addProject.ShortName = ShortName;
            addProject.LongName = LongName;
            addProject.OpenDate = OpenDate;
            addProject.ClosedDate = ClosedDate;
            addProject.IsActive = IsActive;
            //addProject.Client = client;
            //addProject.ClientId = ClientId;
            addProject.Client = client;
            addProject.ClientId = client.Id;
            
            _projectService.Add(addProject);
            //projectService.Add(createdProject);
            var check = client;

            
            project = new Project();
            client = new Client();
            NotifyPropertyChanged("Portfolio");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("project");
            NotifyPropertyChanged("ShortName");
            NotifyPropertyChanged("LongName");
            NotifyPropertyChanged("OpenDate");
            NotifyPropertyChanged("ClosedDate");
            NotifyPropertyChanged("IsActive");
            NotifyPropertyChanged("Client");
            NotifyPropertyChanged("ClientId");
            ToggleCreateProjectForm();
            //var check = Portfolio;

        }
        public void RefreshView()
        {

            NotifyPropertyChanged("Portfolio");
            NotifyPropertyChanged("Roster");
        }
        public void UpdateForm()
        {
            if (project == null)
            {
                return;
            }
            var check = project;
            client = project.Client;
            NotifyPropertyChanged("Portfolio");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("project");
            NotifyPropertyChanged("ShortName");
            NotifyPropertyChanged("LongName");
            NotifyPropertyChanged("OpenDate");
            NotifyPropertyChanged("ClosedDate");
            NotifyPropertyChanged("IsActive");
            NotifyPropertyChanged("Client");
            NotifyPropertyChanged("ClientId");
            ToggleUpdateProjectForm();
        }
        public void Delete()
        {
            if (project == null)
            {
                return;
            }
            ProjectService.Current.Delete(project);
            
            project = new Project();
            client = new Client();
            NotifyPropertyChanged("Portfolio");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("project");
            NotifyPropertyChanged("ShortName");
            NotifyPropertyChanged("LongName");
            NotifyPropertyChanged("OpenDate");
            NotifyPropertyChanged("ClosedDate");
            NotifyPropertyChanged("IsActive");
            NotifyPropertyChanged("Client");
            NotifyPropertyChanged("ClientId");
        }

        public void Update()
        {
            if (project == null)
            {
                return;
            }

            // Update the project properties
            project.ShortName = ShortName;
            project.LongName = LongName;
            project.OpenDate = OpenDate;
            project.ClosedDate = ClosedDate;
            project.IsActive = IsActive;
            project.Client = client;
            project.ClientId = ClientId;
           
            project = new Project();
            client = new Client();
            NotifyPropertyChanged("Portfolio");
            NotifyPropertyChanged("client");
            NotifyPropertyChanged("project");
            NotifyPropertyChanged("ShortName");
            NotifyPropertyChanged("LongName");
            NotifyPropertyChanged("OpenDate");
            NotifyPropertyChanged("ClosedDate");
            NotifyPropertyChanged("IsActive");
            NotifyPropertyChanged("Client");
            NotifyPropertyChanged("ClientId");
            ToggleUpdateProjectForm();
        }

        public bool ProjectMenuVisible
        {
            get => _projectMenuVisible;
            set
            {
                if (_projectMenuVisible != value)
                {
                    _projectMenuVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool CreateProjectFormVisible
        {
            get => _createProjectFormVisible;
            set
            {
                if (_createProjectFormVisible != value)
                {
                    _createProjectFormVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProjectListVisible
        {
            get => _projectListVisible;
            set
            {
                if (_projectListVisible != value)
                {
                    _projectListVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool UpdateProjectFormVisible
        {
            get => _updateProjectFormVisible;
            set
            {
                if (_updateProjectFormVisible != value)
                {
                    _updateProjectFormVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool SearchProjectFormVisible
        {
            get => _searchProjectFormVisible;
            set
            {
                if (_searchProjectFormVisible != value)
                {
                    _searchProjectFormVisible = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public void ToggleUpdateProjectForm()
        {
            UpdateProjectFormVisible = !UpdateProjectFormVisible;
        }

        public void ToggleCreateProjectForm()
        {
            CreateProjectFormVisible = !CreateProjectFormVisible;
        }

        public void ToggleProjectMenu()
        {
            ProjectMenuVisible = !ProjectMenuVisible;
        }

        public void ToggleProjectList()
        {
            ProjectListVisible = !ProjectListVisible;
        }

        public void ToggleSearchProjectForm()
        {
            SearchProjectFormVisible = !SearchProjectFormVisible;
        }
    }
}
