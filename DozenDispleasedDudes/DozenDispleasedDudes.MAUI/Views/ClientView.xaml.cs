using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;

namespace DozenDispleasedDudes.MAUI.Views;


public partial class ClientView : ContentPage
{
	public ClientView()
	{
        InitializeComponent();
        //BindingContext = new ClientMenuViewModel();
        BindingContext = new ClientViewViewModel();
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainMenu");
    }
   
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
       (BindingContext as ClientViewViewModel).RefreshClientList();
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ClientForm");
    }
    private void DetailsClicked(object sender, EventArgs e)
    {
        //Shell.Current.GoToAsync("//ClientDetail");
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }
    private void ProjectsClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewViewModel).RefreshClientList();
    }
    /*
    private void MainClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainMenu");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
    }
    private void DeleteClientClick(object sender, EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Delete();
    }
    private void UpdateClientFormClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).UpdateForm();
    }
    private void OnCreateClientFormClicked(object sender, EventArgs e)
    {

        (BindingContext as ClientMenuViewModel).ToggleCreateClientForm();
    }
    private void OnCreateClientClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Create();
    }
    private void UpdateClientClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientMenuViewModel).Update();
    }
    */

}