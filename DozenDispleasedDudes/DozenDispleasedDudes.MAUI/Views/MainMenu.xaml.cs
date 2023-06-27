namespace DozenDispleasedDudes.MAUI.Views;

public partial class MainMenu : ContentPage
{
    //Is there a way to set a User through out the app similar to tokenization
	public MainMenu()
	{
		InitializeComponent();
	}
    private void ClientClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
    }

    private void ProjectClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Projects");
    }
    private void LogoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void EmployeeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Employees");
    }
}