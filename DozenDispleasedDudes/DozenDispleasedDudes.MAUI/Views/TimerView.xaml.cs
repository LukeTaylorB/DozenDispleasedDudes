using DozenDispleasedDudes.MAUI.ViewModels;

namespace DozenDispleasedDudes.MAUI.Views;

public partial class TimerView : ContentPage
{
    public TimerView(ProjectViewModel pvm, Window parentWindow)
    {
        InitializeComponent();
        BindingContext = new TimerViewModel(pvm,parentWindow);
    }
    //Public constructor for TimerView(Project p)
    private void SubmitClicked(object sender, EventArgs e)
    {
        //Application.Current.CloseWindow(window);
        //Shell.Current.GoToAsync("//Project
    }
}