using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;


namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ClientSave), "clientSave")]
public partial class ProjectView : ContentPage
{
    public int ClientId { get; set; }
    public bool ClientSave { get; set; }
    public ProjectView()
    {
        InitializeComponent();
        //BindingContext = new ProjectMenuViewModel();
        //BindingContext = new ProjectViewViewModel();
    }
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewViewModel(ClientId,ClientSave);
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainMenu");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshProjectList();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewViewModel).RefreshProjectList();
    }

    /*
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).RefreshView();
    }
    private void MainClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainMenu");
    }
    private void OnCreateProjectFormClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).ToggleCreateProjectForm();
    }
    private void OnCreateProjectButtonClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Create();
    }

    private void DeleteProjectClick(object sender, EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Delete();
        
    }
    private void UpdateProjectFormClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).RefreshView();
        
        (BindingContext as ProjectMenuViewModel).UpdateForm();
    }
    private void UpdateProjectClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectMenuViewModel).Update();
        var pCheck2 = (BindingContext as ProjectMenuViewModel).Portfolio;
    }
    */
}