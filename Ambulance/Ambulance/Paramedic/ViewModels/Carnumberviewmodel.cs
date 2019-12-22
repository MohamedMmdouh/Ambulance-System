using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.Paramedic.ViewModels
{
    class Carnumberviewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string carnum = string.Empty;
        public bool isenabled;

        private void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public string CarNum
        {
            get
            {
                return carnum;
            }
            set
            {
                carnum = value;
                if (carnum.Length <2)
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(CarNum));
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
        public Command SubmitCarNum { get; }
        public Carnumberviewmodel()
        {
            SubmitCarNum = new Command(async () => await Submit() ,() => !Isenabled);
        }

        public async Task Submit()
        {

            try {
                Isenabled = false;
                Paramedicdata paramedicdata = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.Ngrok + "Paramedic/SetCarNumber/" + paramedicdata.Id);
                //Get Paramedic ID 
                //
                var content = new StringContent(CarNum, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(paramedicdata.Id + "/" + CarNum, content); //Set Paramedic Id 

                if (response.IsSuccessStatusCode)
                {
                    DependencyService.Get<Toast>().Show("Sumbited");
                    await Task.Delay(2000);
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.Paramedic.Views.ParamedicProfile());
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Wrong Car Num Check it Again!");
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
