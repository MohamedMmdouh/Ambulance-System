using Ambulance.Helper;
using Ambulance.Model;
using Ambulance.Paramedic.Models;
using Ambulance.Paramedic.Services;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.Paramedic.ViewModels
{
    public class HospitalViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Hospitals> hospitalsList { get; set; }
        public Command Getposation { get; }
        public bool isRunning=false;
        Posationdata cordinate;
        HttpClient _client;
        Hospitals hospitals = new Hospitals();

      public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        
        public HospitalViewModel()
        {
            hospitalsList = new ObservableCollection<Hospitals>();
            //Task.Run(async () => { await getposation(); }).Wait();
            Task.Run(async () => { await Gethospitals(); });
            IsRunning = false;
        }

        public async Task Gethospitals()
        {
            try {
            IsRunning = true;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            HubData hub = new HubData();
            hub = JsonConvert.DeserializeObject<HubData>(Settings.permantdata) as HubData;
            cordinate = new Posationdata();

            cordinate.Latitude = hub.Latitude; // 29.972501;//position.Latitude
            cordinate.Longitude = hub.Longitude;// 31.248541;// position.Longitude

            _client = new HttpClient();
            var uri = new Uri(string.Format(Settings.Ngrok+"Paramedic/GetHospitals", string.Empty));
            var json = JsonConvert.SerializeObject(cordinate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    
                    Hospitals[] jsonObject = JsonConvert.DeserializeObject<Hospitals[]>(result);
                    foreach (Hospitals item in jsonObject)
                    {
                        hospitalsList.Add(new Hospitals { HospitalId = item.HospitalId, address = item.address, Name = item.Name, Latitude = item.Latitude, Longitude = item.Longitude, Specialization = item.Specialization });
                    }
                    IsRunning = false;
                }
                else
                {
                    DependencyService.Get<Toast>().Show("There isn't hospitals near to you or Empty");
                    IsRunning = true;
                }
            }
            catch(Exception ex)
            {
                DependencyService.Get<Toast>().Show("There isn't hospitals near");
            }



        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] String PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public class Rootobject
        {
            public List<Hospitals> hospitals { get; set; }
        }
    }
}
