using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(ClientId),"clientId")]
public partial class ClientDetailView : ContentPage
{
	public int ClientId { get; set; }
	public ClientDetailView()
	{
		InitializeComponent();
	}
    public void OkClicked(object sender, EventArgs e)
	{
		(BindingContext as ClientViewModel).AddOrUpdate(); // nulll exception
		Shell.Current.GoToAsync("//Client");

    }
    private void GoBackClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Client");
	}
    private void OnArrived(object sender, EventArgs e)
	{
		BindingContext = new ClientViewModel(ClientId);
		(BindingContext as ClientViewModel).RefreshProjects();
	}
}