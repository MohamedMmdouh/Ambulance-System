using Ambulance.Helper;
using Ambulance.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.ViewModel
{
    class RelativesViewmodel : INotifyPropertyChanged
    {
        string Username = string.Empty;
        string Phonenumber = string.Empty;
        public bool isenabled ;
        public event PropertyChangedEventHandler PropertyChanged;

        public string username
        {
            get
            {
                return Username;
            }
            set
            {
                Username = value;
                if (Username == "")
                {
                    Isenabled = false;
                }
                else
                {
                }
                OnPropertyChanged(username);
            }
        }
        public string phonenumber
        {
            get
            {
                return Phonenumber;
            }
            set
            {
                Phonenumber = value;
                if (Phonenumber == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(phonenumber);
            }
        }
        public Boolean Isenabled
        {
            get
            {
                return isenabled;
            }
            set
            {
                isenabled = value;

                OnPropertyChanged(nameof(Isenabled));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public RelativesViewmodel()
        {
            AddRelative = new Command(async () => await Add(), () => Isenabled);

        }

        public Command AddRelative { get; }

        async Task Add()
        {
            Isenabled = false;
            Relative relative = new Relative();
            relative.isvervied = false;

            //Getting user id
            User user = JsonConvert.DeserializeObject<User>(Helper.Settings.GeneralSettings) as User;

            var client = new HttpClient();
            client.BaseAddress = new Uri(Settings.Ngrok + "Patient/AddPatientRelatives");

            relative.username = username;
            relative.phonenumber = phonenumber;

            String Relativedata = JsonConvert.SerializeObject(relative);
            var content = new StringContent(Relativedata, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("AddPatientRelatives/"+user.ID, content);
            if (response.IsSuccessStatusCode)
            {
                DependencyService.Get<Toast>().Show("new relative Added successfully");
                await Task.Delay(2000);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.UserViews.RelativesList());
                Isenabled = true;
            }
            else
            {
                Isenabled = true;
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error","This Relative is already assosiated to another Account","Ok");
            }
        }
    }
}