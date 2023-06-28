using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;
using DozenDispleasedDudes.Services;

namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectDetailView : ContentPage
{
   
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public ProjectDetailView()
	{
		InitializeComponent();
	}
    private void GoBackClicked(object sender, EventArgs e)
    {
        // should maybe go back to client
        Shell.Current.GoToAsync("//Projects");
        // is there a way to go back to most recent route
    }
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        ////-> this goes to TIMER TICK ?????
       
        BindingContext = new ProjectViewModel(ProjectService.Current.Get(ProjectId));
        (BindingContext as ProjectViewModel).RefreshTimesList();

    }
}