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

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class TimeViewViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Project project { get; set; }
        public ObservableCollection<TimeViewModel> Times 
        { 
            get 
            {
                return new ObservableCollection<TimeViewModel>(timesList.Select(t=> new TimeViewModel(t)).ToList());
            } 
        }
        public ObservableCollection<Time> timesList 
        { 
            get
            {
                if (project == null || project.Id == 0)
                {
                    return new ObservableCollection<Time>(TimeService.Current.Times);
                }
                return new ObservableCollection<Time>(TimeService.Current.Times.Where(t => t.ProjectId == project.Id));
            }
        }
        public TimeViewViewModel(int ProjectId)
        {
            if(ProjectId > 0)
            {
                project = ProjectService.Current.Get(ProjectId);
            }
            else
            {
                project = new Project();
            }
        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Times));
            NotifyPropertyChanged(nameof(timesList));
        }
    }
}
