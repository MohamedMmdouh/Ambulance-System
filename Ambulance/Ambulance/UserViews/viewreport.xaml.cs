using Ambulance.Helper;
using Ambulance.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.UserViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class viewreport : ContentPage
	{
        String IDRecived;
        public viewreport(String ID)
        {
            IDRecived = ID;
            if (ID == null)
            {
                DisplayAlert("Error", "Faild to load layout", "Return");
                Navigation.PopModalAsync();
            }
            else
            InitializeComponent ();
            

        }
        protected override async void OnAppearing()
        {

            HttpClient client =new HttpClient();
            var uri = new Uri(string.Format(Settings.Ngrok+"Report/GetReport/" + IDRecived, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Report report = JsonConvert.DeserializeObject<Report>(content);

                Date.Text=report.CreationTime.ToString();
                diseasname.Text = report.DiseaseName;
                Description.Text = report.Description;
                if (report.IsChronicDisease == false)
                    iscronic.Text = "Cronic Disease";
                else
                    iscronic.Text = "InCronic Disease";
            }
        }

    }
}