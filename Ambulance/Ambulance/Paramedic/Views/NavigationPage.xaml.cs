using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Ambulance.Paramedic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationPage : ContentPage
    {
        HubData hubData;
        double lat;
        double lng;
        HubConnection connection;
        public NavigationPage(HubConnection connection)
        {
            this.connection = connection;
            InitializeComponent ();

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            hubData = JsonConvert.DeserializeObject<HubData>(Settings.permantdata) as HubData;
            var x = hubData;
            lat = Convert.ToDouble(hubData.Latitude);
            lng = Convert.ToDouble(hubData.Longitude);

            await Xamarin.Essentials.Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = "space needle",
                NavigationMode = NavigationMode.Driving

            });

        }
 
        private void ConfirmcClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HospitalsPage(connection));
        }

        private void ViewReports(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Ambulance.Paramedic.Views.ViewReports());
        }
        
        protected override async void OnAppearing()
        {
            hubData = JsonConvert.DeserializeObject<HubData>(Settings.permantdata) as HubData;
            patientname.Text = hubData.PatientName;

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(hubData.Latitude, hubData.Longitude), Distance.FromMiles(3)));
            var Location = new Plugin.Geolocator.Abstractions.Position(position.Latitude, position.Longitude);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(hubData.Latitude, hubData.Longitude),
                Label = "",
                Address = "",
            };
            map.Pins.Add(pin);
        }
        
    }
}

     
