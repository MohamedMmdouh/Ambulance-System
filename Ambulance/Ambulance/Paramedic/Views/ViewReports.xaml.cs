using Ambulance.Paramedic.Models;
using Ambulance.Paramedic.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.Paramedic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewReports : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ViewReports()
        {
            InitializeComponent();
            BindingContext = new ReportsViewmodel();

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as ReportTuble;
            await DisplayAlert("Report", "Disease Description"+details.Description, "Hide");

        }
    }
}
