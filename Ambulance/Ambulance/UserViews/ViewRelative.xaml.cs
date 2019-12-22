using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ambulance.ViewModel;
using Ambulance.Helper;

namespace Ambulance.UserViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewRelative : ContentPage
	{
        public ViewRelative ()
		{
			InitializeComponent ();
            BindingContext = new RelativeupdateViewmodel();
        }

        private async void  Submit_Clicked(object sender, EventArgs e)
        {
            Ambulance.Model.PatientRelatives relative = new Ambulance.Model.PatientRelatives();
            try { 
            HttpClient _client;
            _client = new HttpClient();
            relative.ID =Helper.Settings.permantdata;
            relative.username = relativename.Text;
            relative.phoneNumber = numofrelative.Text;
            var Content = JsonConvert.SerializeObject(relative);
            var content = new StringContent(Content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Settings.Ngrok + "patient/EditPatientRelatives/" + Helper.Settings.permantdata, content);
            if (response.IsSuccessStatusCode)
            {
                DependencyService.Get<Toast>().Show("Updated Successfully");
            }
            else
            {
                DependencyService.Get<Toast>().Show("Try Again Later");
            }
            }
            catch(Exception E)
            {
                DependencyService.Get<Toast>().Show("No internet connection");
            }
        }

    }
}