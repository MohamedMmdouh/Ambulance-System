using Ambulance.Helper;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;
using Ambulance.Model;
using Ambulance.Model.PatientData;

namespace Ambulance.ViewModel
{

    class RequestViewmodel : INotifyPropertyChanged
    {
        //    private HubConnection hubconnection;

        Helper.Connection con = new Helper.Connection();
        Location loc = new Location();

        public bool isenabled;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public RequestViewmodel()
        {
            Requestcommand = new Command(async () => await requestambulace(), () => !Isenabled);
        }



        public bool Isenabled
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
        public Command Requestcommand { get; }


        async Task requestambulace()
        {
            try { 
            var x = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Request", "Are you sure you need to request ambulance car", "yes","No");
            if (x)
            {
                Isenabled = false;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                //getting patient ID 
                Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;
                String patientId = patientdata.ID;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Settings.Ngrok + "Patient/CreateRequest");//replaced with current patient id

                Posationdata cordinate = new Posationdata();

                cordinate.Longitude = 30.818079;
                cordinate.Latitude = 29.153695;

                String Posations = JsonConvert.SerializeObject(cordinate);
                var content = new StringContent(Posations, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("CreateRequest/" + patientId, content);

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.UserViews.Requestview(result));

                }
                else
                {

                    DependencyService.Get<Toast>().Show(result.ToString());
                    Isenabled = true;
                }
            }
            else
            {
                DependencyService.Get<Toast>().Show("Request canceled");
                return;
            }
            }
            catch(Exception e)
            {
                DependencyService.Get<Toast>().Show("No internet connection");
            }
        }
        
    }
}


/*
 * 
 *      private IHubProxy ChatHubProxy;
        public delegate void MessageReceived(string username, string message);
        public event MessageReceived OnMessageReceived;
public RequestViewmodel(string url)
{
    Connection = new HubConnection(url);

    Connection.StateChanged += (StateChange obj) => {
        OnPropertyChanged("ConnectionState");
    };

    ChatHubProxy = Connection.CreateHubProxy("Chat");
    ChatHubProxy.On<string, string>("MessageReceived", (username, text) => {
        OnMessageReceived?.Invoke(username, text);
    });
}
*/
