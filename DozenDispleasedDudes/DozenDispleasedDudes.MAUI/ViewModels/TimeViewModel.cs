using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public decimal? GetCost()
        {
            return TimeService.Current.GetCost(Model);
        }
        public Time Model { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ;
            }
        }
        public string DisplayBill
        {
            get
            {
                return Model.BillString();
            }
        }
        public TimeViewModel()
        {
            Model = new Time();
        }
        public TimeViewModel(Time model)
        {
            Model = model;
            
        }
        //Where tf am I supposed to pass Project to arrive correctly at ViewViewModel to filter 
        public void AddOrUpdate()
        {
            TimeService.Current.AddTime(Model); 
        }
    }
}
