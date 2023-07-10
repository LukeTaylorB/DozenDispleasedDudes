using Microsoft.Maui.Dispatching;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using System.Collections.ObjectModel;

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    // steps through not updating tho 
    public class TimerViewModel : INotifyPropertyChanged
    {
        public Project Project { get; set; }
        private bool _submitButtonVisible;
        private bool _narrativeFieldVisible;
       
        private Window parentWindow;
        private ProjectViewModel _projectViewModel;
        public Time TimeEntry { get; set; }
       
        public Employee selectedEmployee { get; set; }
        public EmployeeViewModel SE { get; set; }
       
        public string TimerDisplay
        {
            get
            {
                TimeSpan elapsedTime = stopwatch.Elapsed;
                string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
                return formattedTime;
            }
        }
        //Maybe switch this to a List of employees not vm
        public ObservableCollection<EmployeeViewModel> EmployeeViewModelList
        {
            get
            {
                return new ObservableCollection<EmployeeViewModel>(EmployeeService.Current.Employees.Select(e => new EmployeeViewModel(e)).ToList());
            }
        }
        public ObservableCollection<Employee> EmployeeList
        {
            get
            {
                return new ObservableCollection<Employee>((EmployeeService.Current.Employees).ToList());
            }
        }


        public bool SubmitButtonVisible
        {
            get => _submitButtonVisible;
            set
            {
                if (_submitButtonVisible != value)
                {
                    _submitButtonVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void ToggleSubmitButton()
        {
            SubmitButtonVisible = !SubmitButtonVisible;
        }
        public string ProjectDisplay
        {
            get
            {
                return Project.TimerString();
            }
        }

        private IDispatcherTimer timer { get; set; }
        private Stopwatch stopwatch { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand SetEmployeeCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand SubmitCommand { get; private set; }
        public void ExecuteStart()
        {
            if(SubmitButtonVisible == true)
            {
                ToggleSubmitButton();
            }
            stopwatch.Start();
            if(TimeEntry.start == null)
            {
                TimeEntry.start = DateTime.Now;
            }
            timer.Start();
        }
        public void ExecuteSetEmployee()
        {

        }
        public void ExecuteStop()
        {
            TimeEntry.Stop = DateTime.Now;
            stopwatch.Stop();
            if(SubmitButtonVisible == false)
            {
                ToggleSubmitButton();
            }
        }

        public void ExecuteSubmit()
        {
            decimal minutes = ((decimal)stopwatch.Elapsed.Seconds / 60);
            TimeEntry.Hours = (minutes / 60);
            TimeEntry.IsSelected = false;
           
            
            //var EmployeeId = SelectedEmployee.Id; //exception
            // var check = employee;
            //var check = TimeEntry.Employee;
            TimeEntry.EmployeeRate = TimeEntry.Employee.Rate;
            TimeEntry.EmployeeId = TimeEntry.Employee.Id;
            TimeEntry.Cost = TimeService.Current.GetCost(TimeEntry);

            TimeService.Current.AddTime(TimeEntry);
            _projectViewModel.RefreshTimesList();
            timer.Stop();
            Application.Current.CloseWindow(parentWindow); 
            
            //OnSubmitCompleted();
        }
        private void SetupCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
            SubmitCommand = new Command(ExecuteSubmit);
        }
        public TimerViewModel(ProjectViewModel pvm, Window parentWindow)
        {
            _projectViewModel = pvm;
            TimeEntry = new Time();
            TimeEntry.ProjectId = pvm.Model.Id;
            
            Project = ProjectService.Current.Get(pvm.Model.Id) ?? new Project();
            stopwatch = new Stopwatch();
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsRepeating = true;

            timer.Tick += Timer_Tick;
            SetupCommands();
            this.parentWindow = parentWindow;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsRunning)
            {
                //timer does not stop counting on debug super wierd.
                var check = stopwatch.Elapsed.Seconds;
                NotifyPropertyChanged(nameof(TimerDisplay));
                //this is not working visually ?
            }
        }
        /*
        protected virtual void OnSubmitCompleted()
        {
            SubmitCompleted?.Invoke(this, EventArgs.Empty);
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
