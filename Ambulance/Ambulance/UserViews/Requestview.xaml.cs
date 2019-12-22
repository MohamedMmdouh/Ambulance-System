using Ambulance.Model;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.UserViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Requestview : ContentPage
	{
        double lat;
        double lng;
		public Requestview (String Request)
        {
            Requestdata requestdata = JsonConvert.DeserializeObject<Requestdata>(Request) as Requestdata;
            lat = Convert.ToDouble(requestdata.ParamedicCoordinates.Latitude);
            lng = Convert.ToDouble(requestdata.ParamedicCoordinates.Longitude);

            InitializeComponent ();
            
            paramedicname.Text = requestdata.ParamedicName.ToString();
            Duration.Text = requestdata.AuthorityLocation.ToString();
            if (requestdata.TimeInSeconds > 60)
            {
                double hours = requestdata.TimeInSeconds / 3600;
                double min = (hours % 1)*60;
                time.Text =Convert.ToInt32(hours)+" Hours"+ Convert.ToInt32(min) + " Minutes";
            }
            else
            {
                time.Text = requestdata.TimeInSeconds/60+ " Minutes";
            }
            Distance.Text =(requestdata.Distance).ToString()+" Mile";
        }

        private async void Trackonmap_Clicked(object sender, EventArgs e)
        {

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = "space needle",
                NavigationMode = NavigationMode.Driving
            });
        }
    }
}