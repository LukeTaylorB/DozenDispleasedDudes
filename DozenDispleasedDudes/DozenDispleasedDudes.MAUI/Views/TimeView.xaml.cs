using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;


namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(ProjectId), "projectId")]

public partial class TimeView : ContentPage
{
	public int ProjectId { get; set; }
	public TimeView()
	{
		InitializeComponent();
	}
}