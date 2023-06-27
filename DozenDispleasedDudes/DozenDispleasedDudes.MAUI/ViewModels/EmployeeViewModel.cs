using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Library.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace DozenDispleasedDudes.MAUI.ViewModels
{
    public class EmployeeViewModel
    {
        
        public Employee Model { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddEmployeeCommand { get; private set; }
        public void SetupCommands()
        {
            DeleteCommand = new Command(
               (e) => ExecuteDelete((e as EmployeeViewModel).Model.Id));
            
            EditCommand = new Command(
                (e) => ExecuteEdit((e as EmployeeViewModel).Model.Id)); // What is up with this no issue with delete
            AddEmployeeCommand = new Command(
                (c) => ExecuteAddEmployee());
        }
        public void ExecuteAddEmployee()
        {
            var check = Model;
            AddOrUpdate(); //save the client so that we have an id to link the project to
            //TODO: if we cancel the creation of this client, we need to delete it on cancel.
            Shell.Current.GoToAsync($"//EmployeeDetail?employeeId={Model.Id}");
        }
        public void ExecuteDelete(int id)
        {
            EmployeeService.Current.RemoveEmployee(id);
        }
        public void ExecuteEdit(int id)
        {
            //EmployeeService.Current.GetEmployeeById(e.Id); my getter function
            var check = id;
            //var modelCheck = Model
            Shell.Current.GoToAsync($"//EmployeeDetail?employeeId={id}");
        }
        public EmployeeViewModel(Employee employee) 
        {
            Model = employee;
            // on menu click "admin" base client first call this
            SetupCommands();
        }
        public EmployeeViewModel(int employeeId)
        {
            var check = employeeId;
            if (employeeId == 0)
            {
                Model = new Employee();
            }
            else
            {
                Model = EmployeeService.Current.Get(employeeId);
            }
            SetupCommands();
        }
        public EmployeeViewModel()
        {
            Model = new Employee();
            //this then getss called :( which is where it gets set to null neither make sense. ON second call Model data= {2,}
            SetupCommands();
        }
        public void AddOrUpdate()
        {
            var check = Model; 
            EmployeeService.Current.AddOrUpdate(Model);
        }
        public void RefreshEmployees()
        {
            //NotifyPropertyChanged(nameof(Employees));
        }
    }
}
