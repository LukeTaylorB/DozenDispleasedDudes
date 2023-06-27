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

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    // steps through not updating tho 
    public class TimerViewModel : INotifyPropertyChanged
    {
        public Project Project { get; set; }
        private bool _submitButtonVisible;
        private bool _narrativeFieldVisible;
        public Time TimeEntry { get; set; }
        public string TimerDisplay
        {
            get
            {
                TimeSpan elapsedTime = stopwatch.Elapsed;
                string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);


                return formattedTime;
                /*
                return string.Format("{0:00}:{0:00}:{1:00}",
              stopwatch.Elapsed.Hours,
              stopwatch.Elapsed.Minutes,
              stopwatch.Elapsed.Seconds);
                */
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
            TimeService.Current.AddTime(TimeEntry);
            //OnSubmitCompleted();
        }
        private void SetupCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
            SubmitCommand = new Command(ExecuteSubmit);
        }
        public TimerViewModel(int projectId)
        {
            TimeEntry = new Time();
            TimeEntry.ProjectId = projectId;
            
            Project = ProjectService.Current.Get(projectId) ?? new Project();
            stopwatch = new Stopwatch();
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsRepeating = true;

            timer.Tick += Timer_Tick;
            SetupCommands();
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
