using DozenDispleasedDudes.Library.DTO;
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
            if (Model.IsActive == false && Model.ClosedDate == null)
            {
                Model.ClosedDate = DefaultDate;
            }

            SetupCommands();
        }
        
        public ClientViewModel(int clientId)
        {
            if(clientId == 0)
            {
                Model = new ClientDTO();
                IA = Model.IsActive;
                NotifyPropertyChanged(nameof(IA));
                CD = Model.ClosedDate;
                NotifyPropertyChanged(nameof(CD));
            }
            else
            {
                Model = ClientService.Current.Get(clientId);
                IA = Model.IsActive;
                NotifyPropertyChanged(nameof(IA));
                CD = Model.ClosedDate;
                NotifyPropertyChanged(nameof(CD));
            }
            SetupCommands();
        }
        /*
       reason for the ghetto rig toxic back and forth just for a xamlto reset closed date visualy 
          data functionally behaves correctly on back end. the correct auto update in backend occurs if no new closed date is selected
          It has been working since 7 currently @ 1:26 probaly for longer just locked in certainy on that fact after dinner at 7
         problem occurs vissualy when/if obj has been set to closed whether it before the default date or manually.
         (i know it ocurs this way because of the midnight threshold causing same issue)Once the object set to active
         after bieng closed date picker refuses to show default date even when closed date set to null
         forcefully if client is active just before AddOrUpdate
         Once set back to closed it holds this phantom date which im trying to discover how its even remebering let alone 
         ck 2 byzantium level usurpering things that have no issues else where.
        The incredibly frustrating this is this only occurs in that specific order if you update client.ClosedDate if its closed already no Issues.
         I know its a very minor detail but I really want to care about those minor ones because of the things we talked about in class this morning
         IMO yes in it a minor detail for a coding project. Where it will either provide confusion if left in orginal state.
         in the case that someone accidently activates a client and would be thankful the it stored the old one as place holder.
         only to be upset when it behaves as intended when reclosing 
         However it could also be anoying having to scroll all the way to the default date on closure if any new work was done justifying 
         the default date(day of action) as new closed date. 
         My inital intent was to just slap a convience button that set a property on whether u want today or a date in the past. 
         However while brain storming the property and doing some minor testing I noticed I can set future dates.
         Which gave me an idea for a potential extension of the features. So I decided to build the property as the base.
         then go back and flesh it out 
         potential additions  bieng
         save old closed date make sure if a project/time entry has been submited since reopen that is the oldest u can set closed date
         which can be fixed with a check on last time entry and setting that as the min date. 
          -requires an addition of a property to project that saves Date of creation which can be the sub check if timeService List for projects related to client is empty
         then have a button to set date picker for whichever of the two gets the short straw on not bieng the default
         */

        private bool ia;
        public bool IA
        {
            get => ia;
            set
            {
                if (ia != value)
                {
                    if(value == false)
                    {
                        ia = value;
                        CD = DefaultDate;
                        NotifyPropertyChanged(nameof(CD));
                        NotifyPropertyChanged(nameof(IA));
                        Model.IsActive = IA;
                        NotifyPropertyChanged(nameof(Model.IsActive));
                    }
                    else
                    {
                        ia = value;
                        NotifyPropertyChanged(nameof(IA));
                        Model.IsActive = IA;
                        NotifyPropertyChanged(nameof(Model.IsActive));
                        CD = null;
                        NotifyPropertyChanged(nameof(CD));
                    }
                }
            }
        }
        private DateTime? cd;
        
        public DateTime? CD
        {
            get => cd;
            
            set 
            {   
                if (IA == true)
                {
                    cd = null;
                    Model.ClosedDate = cd;
                    NotifyPropertyChanged(nameof(Model.ClosedDate));
                    NotifyPropertyChanged(nameof(CD));
                }
                else if (IA == false)
                {
                    if (cd != value)
                    {
                        if (  Model.Id == 0 )
                        {
                            Model.ClosedDate = DefaultDate;
                            cd = Model.ClosedDate;
                            NotifyPropertyChanged(nameof(Model.ClosedDate));
                            NotifyPropertyChanged(nameof(CD));
                        }
                        else 
                        { 
                            cd = value;
                            Model.ClosedDate = cd;
                            NotifyPropertyChanged(nameof(Model.ClosedDate));
                            NotifyPropertyChanged(nameof(CD));
                        }
                    }
                }
                else 
                { cd = null; }
            }
        }
        public ClientViewModel(ClientDTO client)
        {
            Model = client;
            
            if (Model.IsActive == false)
            {
                if(Model.ClosedDate == null)
                {
                    Model.ClosedDate = DefaultDate;
                    NotifyPropertyChanged(nameof(Model.ClosedDate));
                }
            }
            
            SetupCommands();
        }

        public void AddOrUpdate()
        {
            ClientService.Current.AddOrUpdate(Model);
        }
        
       

    }
}
