using Ambulance.Helper;
using Ambulance.Model;
using Newtonsoft.Json;
using Plugin.Media;
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
	public partial class Registeration : ContentPage
	{
        User user = new User();
        bool isenabled;
		public Registeration ()
		{
			InitializeComponent ();
            BindingContext = user;
            Login();
		}
        private async void uploadpictureAsync(object sender, EventArgs e)
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Uploads", "Picking a photo is not allowed", "Ok");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            imageToUpload.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void RegisterBtn_Clicked(object sender, EventArgs e)
        {
            try { 
            isenabled = false;
            string userdata = "";
            Boolean isvalid = true;
            if (Username.Text==""|| Username.Text.Length < 5)
            {
                requser.IsVisible = true;
                isvalid = false;
            }
            if (Email.Text=="" ||Email.TextColor==Color.Red)
            {
                reqemail.IsVisible = true;
                isvalid = false;
            }
            if (Password.Text=="" || Password.Text.Length < 8)
            {
                reqpass.IsVisible = true;
                isvalid = false;
            }

            if (nationalid.Text.Length != 14)
            {
                reqssn.IsVisible = true;
                isvalid = false;
            }
            if (phonenum.Text.Length != 11)
            {
                reqphonenum.IsVisible = true;
                isvalid = false;
            }
            if (Birthdate.ToString() == "")
            {
                reqAge.IsVisible = true;
                isvalid = false;
            }
            if (isvalid)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.Ngrok+"Patient/Registration");
                userdata = JsonConvert.SerializeObject(user);
                var content = new StringContent(userdata, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync("Registration", content);


                if (response.IsSuccessStatusCode)
                 {
                     await Navigation.PopModalAsync();
                     await Navigation.PushModalAsync(new MainPage());
                 }
                else
                {
                    DependencyService.Get<Toast>().Show("Faild");
                }
            }
            else
            {
                DependencyService.Get<Toast>().Show("Check Data Again");
            }
            }
            catch(Exception exception)
            {
                DependencyService.Get<Toast>().Show("No internet connection");
            }
            isenabled = true;
        }
        private void Login()
        {
            login.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushModalAsync(new Ambulance.MainPage());
                })
            });
        }
    }
}
