using Ambulance.Helper;
using Ambulance.Model;
using Ambulance.Paramedic.Models;
using Ambulance.Validation;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.Viewmodel
{
    class Loginviewmodel : INotifyPropertyChanged
    {
        Emailpattern checker = new Emailpattern();

        string email = string.Empty;
        string password = string.Empty;
        public bool isenabled;
        public bool isrunning = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string email = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(email));
        }
        private void OnPropertyChangedP([CallerMemberName] string password = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(password));
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;

                if (!checker.isverfied(email))
                {
                    Isenabled = false;
                }
                else
                {
                }
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;

                if (password == "" || password.Length < 8)
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Password));
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

        public Boolean IsRunning
        {
            get
            {
                return isrunning;
            }
            set
            {
                isrunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        public Loginviewmodel()
        {
            Logincommand = new Command(async () => await Login(), () => !Isenabled);
        }
   
        public Command Logincommand { get; }
     
        async Task Login()
        {
            Isenabled = false;
            IsRunning = true;
            try { 
            var client = new HttpClient();
            client.BaseAddress = new Uri(Settings.Ngrok+"Users");

            User user = new User();
            user.Email = email;
            user.Password = password;

            String Userdata = JsonConvert.SerializeObject(user);
            var content = new StringContent(Userdata, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("users/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Loginmodel modeldata = JsonConvert.DeserializeObject<Loginmodel>(result);
                Settings.GeneralSettings =result;

                if (modeldata.RoleName == "Patient")
                {

                        client = new HttpClient();
                        var uri = new Uri(string.Format(Settings.Ngrok+"patient/GetPatientByUserId/" + modeldata.ID));//+ loginmodel.ID
                        response = await client.GetAsync(uri);
                        if (response.IsSuccessStatusCode)
                        {
                            var loginresult = await response.Content.ReadAsStringAsync();
                            Settings.GeneralSettings = loginresult;
                            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.UserViews.UserViews());
                        }
                        else
                        {
                                DependencyService.Get<Toast>().Show("Faild to connect server");
                        }

                }
                else
                {
                    var uri = new Uri(string.Format(Settings.Ngrok+"Paramedic/GetParamedicByUserId/" + modeldata.ID));
                     response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var paramedicdata = await response.Content.ReadAsStringAsync();
                        Settings.GeneralSettings = paramedicdata;
                        Paramedicdata paramedic = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;
                        DependencyService.Get<Toast>().Show("Hello " + paramedic.UserData.Username.ToString());
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.Paramedic.Views.EnterCarNo());
                        return;
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Faild to connect server");
                    }
                }
              
            }
            else
            {
                DependencyService.Get<Toast>().Show("Invaild Email or Password");
                Isenabled = true;
                IsRunning = false;
                return;
            }
            }
            catch(Exception x)
            {
                Isenabled = true;
                IsRunning = false;
                DependencyService.Get<Toast>().Show("No internet connection");
            }
        }

     
    }
}