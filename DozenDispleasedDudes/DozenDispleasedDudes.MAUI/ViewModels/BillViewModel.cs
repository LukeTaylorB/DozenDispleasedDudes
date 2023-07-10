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
    public class BillViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Build TimeViewModelList from timeList
        public ObservableCollection<TimeViewModel> tvmList 
        { 
            //turns timeList into a ViewModel
            get
            {
                return new ObservableCollection<TimeViewModel>(Model.timeList.Select(t=> new TimeViewModel(t)));
            }
        }
        public string clientShortString
        {
            get
            {
                return Model.client.shortString(); 
            }
        }

        public string TotalCostDisplay 
        { 
            get
            {
                return $"TotalCost: ${(Model.TotalCost).ToString("F2")}";
            } 
        }
        public Bill Model { get; set; }
        public ICommand SendCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public void executeSend()
        {
            var Id = Model.client.Id;
            BillService.Current.send(Model);
            Shell.Current.GoToAsync($"//ClientDetail?clientId={Id}");
        }
        public void executeCancel()
        {
            var Id = Model.client.Id;
            BillService.Current.cancel(Model);
            Shell.Current.GoToAsync($"//ClientDetail?clientId={Id}");
        }
        public void SetupCommands()
        {
            SendCommand = new Command(
                (b) => executeSend());
            CancelCommand = new Command(
                (b) => executeCancel());

        }
        public BillViewModel(Bill model) 
        {
            Model = model;
           
            SetupCommands();
        }

    }
}
