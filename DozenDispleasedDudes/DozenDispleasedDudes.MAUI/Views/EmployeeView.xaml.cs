using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;

public partial class EmployeeView : ContentPage
{
	public EmployeeView()
	{
		InitializeComponent();
		BindingContext = new EmployeeViewViewModel();
	}
    //goal list view with search bar just click on add edit remove for each employee(simple and straight forward)
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshEmployeeList();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainMenu");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        //this gets called after 2nd constructor in Stack trace Model data still equals {2,}
        //var check = (BindingContext as EmployeeViewViewModel).Model;
        Shell.Current.GoToAsync("//EmployeeDetail");
    }
    

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshEmployeeList();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewViewModel).RefreshEmployeeList();
    }
}