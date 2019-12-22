using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Ambulance.ParamedicViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Paramadic : TabbedPage
    {
		public Paramadic ()
		{
			InitializeComponent ();
            

            picn.Source = ImageSource.FromFile("person.png");

            Pnav.Clicked += (s, e) =>
            {
                CurrentPage = Children[1];
            };

            String[] hospitals = { "Dokki", "Giza", "Harm", "Faisal", "Talbia", "Dokki", "Giza", "Harm", "Faisal", "Talbia", "Dokki", "Giza", "Harm", "Faisal", "Talbia" };
            hospitalLists.ItemsSource = hospitals;
            hospitalLists.ItemSelected += (s, e) =>
            {
                CurrentPage = Children[1];
            };
        }

        private async void Request_view(object sender, EventArgs e)
        {

            Request.IsEnabled = true;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(position.Latitude, position.Longitude), Distance.FromMiles(3)));
            var Location = new Position(position.Latitude, position.Longitude);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(position.Latitude, position.Longitude),
                Label = "",
                Address = "",
                Id = "User position"
            };
            map.Pins.Add(pin);

            /*    var location = new Location(47.645160, -122.1306032);
                var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

                await Map.OpenAsync(location, options);
                */
        }
        /*
        protected override voi()
        {

        }
        */

    }
}
