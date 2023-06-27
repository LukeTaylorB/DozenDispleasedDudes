using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;

public partial class TimerView : ContentPage
{
    public TimerView(int projectId)
    {
        InitializeComponent();
        BindingContext = new TimerViewModel(projectId);
    }
    private void SubmitClicked(object sender, EventArgs e)
    {
        //Application.Current.CloseWindow(window);
        //Shell.Current.GoToAsync("//Project
    }
}