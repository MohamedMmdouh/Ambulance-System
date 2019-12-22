using Ambulance.Model;
using Ambulance.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Patient_history : ContentPage
    {

        public Patient_history()
        {
            InitializeComponent();
            BindingContext = new ReportViewmodel();
           

        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                try
                {
                    var details = e.Item as Reportdata;

                    await Navigation.PushModalAsync(new viewreport(details.ID));
                }
                catch (Exception exception)
                {
                    DependencyService.Get<Toast>().Show("Faild to Load data");
                }
            }
            else
                return;
        }
    }
}
