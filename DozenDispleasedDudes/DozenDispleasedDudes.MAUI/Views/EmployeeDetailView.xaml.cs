using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(EmployeeId),"employeeId")]
public partial class EmployeeDetailView : ContentPage
{
	public int EmployeeId { get; set; }
	public EmployeeDetailView()
	{
		InitializeComponent();
        //BindingContext = new EmployeeViewModel(); not sure why this was here but third constructor not bieng called anymore
        //var check = (BindingContext as EmployeeViewModel).Model;

    }
	public void OkClicked(object sender, EventArgs e)
	{
		
        (BindingContext as EmployeeViewModel).AddOrUpdate();
        Shell.Current.GoToAsync("//Employees");
       
    }
	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Employees");
	}
	private void OnArriving(object sender, EventArgs e)
	{
        //var check = (BindingContext as EmployeeViewModel).Model;
        // comes here next model still same
        BindingContext = new EmployeeViewModel(EmployeeId); //error on employeeId
		(BindingContext as EmployeeViewModel).RefreshEmployees();

    }
}