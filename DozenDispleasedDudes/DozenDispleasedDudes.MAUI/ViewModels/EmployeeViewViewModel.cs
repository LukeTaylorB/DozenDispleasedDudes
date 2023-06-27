using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using DozenDispleasedDudes.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DozenDispleasedDudes.Services;

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class EmployeeViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Employee SelectedEmployee { get; set; }


        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                return new ObservableCollection<EmployeeViewModel>(EmployeeService.Current.Employees.Select(e => new EmployeeViewModel(e)).ToList());
            }
        }
        public void Delete()
        {
            if (SelectedEmployee != null)
            {
                EmployeeService.Current.RemoveEmployee(SelectedEmployee.Id);
                SelectedEmployee = null;
                NotifyPropertyChanged(nameof(Employees));
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }
        public void RefreshEmployeeList()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        
    }
}

