using DozenDispleasedDudes.Library.Services;
using DozenDispleasedDudes.MAUI.ViewModels;
using DozenDispleasedDudes.Models;

namespace DozenDispleasedDudes.MAUI.Views;

[QueryProperty(nameof(BillId), "billId")]
public partial class BillDetailView : ContentPage
{
	public int BillId { get; set; }
	public BillDetailView()
	{
		InitializeComponent();
	}
	private void OnArrived(object sender, EventArgs e)
	{
		BindingContext = new BillViewModel(BillService.Current.Get(BillId));
	}
}