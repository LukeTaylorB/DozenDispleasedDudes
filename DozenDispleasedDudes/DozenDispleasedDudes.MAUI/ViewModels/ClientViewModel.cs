﻿using DozenDispleasedDudes.Library.DTO;
using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
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
        public DateTime DefaultDate = DateTime.Today;
        public ClientDTO Model { get; set; }
        
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
       


        //Change this to a timeViewModel
        public ObservableCollection<Time> TimeList
        {
            get
            {
                return new ObservableCollection<Time>(TimeService.Current.Times.Where(t => t.IsSelected == true && (t.BillId == 0 || t.BillId == null)));
            }
        }
        public string DisplayShort
        {
            get
            {
                return Model.shortString();
            }
        }
        
        

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }
        public ICommand GenerateBillCommand { get; private set; }
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
        public void ExecuteGenerateBill()
        {
           //we need TotalCost make sure Times Have Cost To see that we need timeList to be 
           // a List<TimeViewModel> not a List<Time> 
           // somwhere in bill we need a list of ProjectIds and maybe even the ProjectsList
            var Invoice = new Bill();
            Invoice.timeList = TimeList.ToList(); 
            
            Invoice.client = new Client(Model); // How DO I get and set correctly; bec
            var check = Invoice.InvoiceId;
            
            
            BillService.Current.AddOrUpdate(Invoice); // add functionaility to bill correction Here
            TimeService.Current.UnselectByList(Invoice.timeList);
            Shell.Current.GoToAsync($"//BillDetail?billId={Invoice.InvoiceId}");
            //Introduce Time Variable for on a bill and set here 
            //Then Deselect; 
            // adjust Time List getter to check for on a bill var and dont add if true. set default = false;



        }
        public void RefreshTimes()
        {
            NotifyPropertyChanged(nameof(TimeList));
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
            GenerateBillCommand = new Command(
                (b) => ExecuteGenerateBill());
            ShowProjectsCommand = new Command(
                (c) => ExecuteShowProjects((c as ClientViewModel).Model.Id));
        }
        public ClientViewModel() 
        {
            Model = new ClientDTO();
           
            SetupCommands();
        }
        public ClientViewModel(int clientId)
        {
            if(clientId == 0)
            {
                Model = new ClientDTO();
            }
            else
            {
                Model = ClientService.Current.Get(clientId);
            }
            SetupCommands();
        }
        public ClientViewModel(ClientDTO client)
        {
            Model = client;
            SetupCommands();
        }

        public void AddOrUpdate()
        {
            var check = Model.IsActive;
            if (Model.IsActive == true)
            {
                Model.ClosedDate = null;
            }
            ClientService.Current.AddOrUpdate(Model);
        }
    }
}
