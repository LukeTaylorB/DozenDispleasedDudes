using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;

namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(ClientSave), "clientSave")]


public partial class ProjectFormView : ContentPage
{
    public bool ClientSave { get; set; }
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public ProjectFormView()
	{
		InitializeComponent();
	}
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        if (ProjectId == 0)
        {
            BindingContext = new ProjectViewModel(ClientId);
        }
        else
        {
            BindingContext = new ProjectViewModel(ProjectService.Current.Get(ProjectId));
        }
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
        // is there a way to go back to most recent route
    }
}