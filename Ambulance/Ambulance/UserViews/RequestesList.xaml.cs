using Ambulance.Helper;
using Ambulance.Model.PatientData;
using Ambulance.PopUp;
using Ambulance.ViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestesList : ContentPage
    {
        public RequestesList()
        {
            InitializeComponent();
            BindingContext = new RequestlistViewmodel();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            else
            {
                var details = e.Item as PatientRequests;
                await DisplayAlert("Request Data", "Creation date of request :"+details.CreationTime, "Hide");
            }

        }

    }
}
