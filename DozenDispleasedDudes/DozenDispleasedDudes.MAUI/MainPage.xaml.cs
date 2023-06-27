using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

// you can overide Binding context for an idividual grid but You can not have multiple fo a single page
//Refreesh view form entry empty also where to reset selected person

namespace DozenDispleasedDudes.MAUI
{
    public partial class MainPage : ContentPage
    {
        //public String user { get; set; }
        //public Employee ActiveEmployee { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            
        }
        /*
        private void OnLoginClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(user))
            {
                var employeeService = EmployeeService.Current;
                var employee = employeeService.Employees.FirstOrDefault(emp => emp.Name == user) ;

                if (employee != null)
                {
                    
                    Shell.Current.GoToAsync("//MainMenu");

                }
                else
                {
                    // Display an error message
                    DisplayAlert("Error", "Invalid username", "OK");
                }
            }
              
        }
        */
        
        private void MainClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainMenu");
        }
        



    }
}
