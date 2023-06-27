using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;


[QueryProperty(nameof(ClientId), "clientId")]
public partial class ClientFormView : ContentPage
{
    public int ClientId { get; set; }
    public ClientFormView()
	{
		InitializeComponent();
	}
    public void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).AddOrUpdate(); // nulll exception
        Shell.Current.GoToAsync("//Client");

    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
    }
    private void OnArriving(object sender, EventArgs e)
    {
        BindingContext = new ClientViewModel(ClientId);
        //(BindingContext as ClientViewModel).RefreshClientList();
    }
}