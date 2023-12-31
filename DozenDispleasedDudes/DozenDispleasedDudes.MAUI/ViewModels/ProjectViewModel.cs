﻿using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using DozenDispleasedDudes.MAUI.Views;
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
    
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Time> timeEntries
        {
            get
            {
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<Time>(TimeService.Current.Times);
                }
                return new ObservableCollection<Time>(TimeService.Current.Times.Where(t => t.ProjectId == Model.Id && (t.BillId == 0 || t.BillId == null)));
            }
        }
        public ObservableCollection<TimeViewModel> TimeEntries
        {
            get
            {
                return new ObservableCollection<TimeViewModel>(timeEntries.Select(c => new TimeViewModel(c)).ToList());
            }
        }

        public void RefreshTimesList()
        {
            NotifyPropertyChanged(nameof(TimeEntries));
          
        }

        public DateTime DefaultDate = DateTime.Today; //remember field not property 
        public Project Model {get; set; } 
        
        public Time TimeModel { get; set; }
        
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DetailsCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }

        public string Display
        {
            get
            {
                return Model.ToString();
            }
        }
        public string projName
        {
            get
            {
                 return Model.Name();
            }
        }
        public string TimeEntriesLabel
        {
            get
            {
                return ( "\t" + (Model.ShortName) + "'s TimeEntries");
            }
        }
        // Function and property to set is selected for all time Entries when project isSelected
        public void TimeProjectIsSelectedSet(bool value)
        {
            foreach (Time t in timeEntries )
            {
                t.IsSelected = value;
                NotifyPropertyChanged(nameof(t.IsSelected));
            }
            RefreshTimesList();
        }
        private bool ps;

        public bool PS
        {
            get => ps;
            set
            {
                if(ps != value)
                {
                    ps = value;
                    Model.IsSelected = value;
                    NotifyPropertyChanged(nameof(Model.IsSelected));
                    NotifyPropertyChanged(nameof(ps));
                    TimeProjectIsSelectedSet(value);
                }
            }
        }

        private void ExecuteAdd()
        {
            //Project exists 
            var projectExists = Model.Id; // if 0 go to client after else go to projects
            var clientViewOrigin = Model.ClientId; 
            AddOrUpdate();
            //ProjectService.Current.Add(Model);
            if(projectExists == 0)
            {
                Shell.Current.GoToAsync($"//ClientDetail?clientId={Model.ClientId}");
            }
            else
            {
                Shell.Current.GoToAsync($"//Projects?clientId={Model.ClientId}");
                //Shell.Current.GoToAsync("//Projects");
            }
            
        }
        public void ExecuteDelete(int id)
        {
            ProjectService.Current.Delete(id);
        }
        public void ExecuteEdit(int id )
        {
            Shell.Current.GoToAsync($"//ProjectForm?clientId={Model.ClientId}&projectId={id}");
        }
        public void ExecuteDetails(int id)
        {
            Shell.Current.GoToAsync($"//ProjectDetail?projectId={id}");
        }
        public void SetupCommands()
        {
            AddCommand = new Command(ExecuteAdd);
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ProjectViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ProjectViewModel).Model.Id));
            DetailsCommand = new Command(
                (c) => ExecuteDetails((c as ProjectViewModel).Model.Id));
            TimerCommand = new Command(ExecuteTimer);
        }
        public ProjectViewModel()
        {
            Model = new Project();
            SetupCommands();
        }

        public ProjectViewModel(int clientId)
        {
            var client = ClientService.Current.Get(clientId);
            Model = new Project { Client = new Client(client), ClientId = clientId }; // should I be changing the fundamentals of project here.
          
            SetupCommands();
        }

        public ProjectViewModel(Project model)
        {
            
            Model = model;
            SetupCommands();
        }
        public void AddOrUpdate()
        {
            var check = Model.IsActive;
            ProjectService.Current.AddOrUpdate(Model);
        }
        private void ExecuteTimer()
        {
            var window = new Window()
            {
                Width = 675,
                Height = 500,
                X = 0,
                Y = 0
            };
            var view = new TimerView(this,window);
            window.Page = view; 
            

            Application.Current.OpenWindow(window);

        }

    }
}
