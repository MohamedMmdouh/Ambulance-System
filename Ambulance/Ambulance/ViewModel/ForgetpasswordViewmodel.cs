using Ambulance.Helper;
using Ambulance.Model;
using Ambulance.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.ViewModel
{
    class ForgetpasswordViewmodel : INotifyPropertyChanged
    {
        Emailpattern checker = new Emailpattern();

        public event PropertyChangedEventHandler PropertyChanged;
        string email = string.Empty;
        public bool isenabled=false;

        private void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
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
                if (!checker.isverfied(email) || email=="")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Email));
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

      
        public Command Resetcommand { get; }
        public ForgetpasswordViewmodel()
        {
            Resetcommand = new Command(async () => await Reset());
        }

        public async Task Reset()
        {
            try { 
                    Isenabled = false;

                    var client = new HttpClient();
                    String data= "'"+Email+ "'";
                 
                    var uri = new Uri(string.Format(Settings.Ngrok + "Users/ForgetPassword/"+ data.ToString(), string.Empty)); 
                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        DependencyService.Get<Toast>().Show("Email send Successfully");
                        await Task.Delay(2000);
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.MainPage());
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Check Your email again");
                        Isenabled = true;
                        return;
                    }
            }
            catch (Exception e)
            {
                DependencyService.Get<Toast>().Show("No internet connection");
            }
        }
    }
}
