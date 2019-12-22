using Ambulance.Paramedic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;

namespace Ambulance.Paramedic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParamedicProfile : ContentPage
	{
		public ParamedicProfile ()
		{
			InitializeComponent ();
            BindingContext = new Paramedicprofile();
        }

        private async void Logout(object sender, EventArgs e)
        {
            Paramedicdata paramedicdata = new Paramedicdata();
            paramedicdata = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;
            
                HttpClient _client;
                _client = new HttpClient();
                var uri = new Uri(string.Format(Settings.Ngrok+ "paramedic/paramedicloggingout/"+paramedicdata.Id, string.Empty));
                await _client.GetAsync(uri);
                await Navigation.PushModalAsync(new MainPage());

        }
        protected override void OnDisappearing()
        {
            Navigation.PopModalAsync();
        }

    }
}