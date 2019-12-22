using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
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
    class FailResonViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FailReasons> failReasons { get; set; }
        HttpClient _client;

        public FailResonViewmodel()
        {
            try { 
            failReasons = new ObservableCollection<FailReasons>();
            Task.Run(async () => { await Getreasons(); });
            }
            catch (Exception)
            {
                DependencyService.Get<Toast>().Show("No Internet connection");
            }
        }

        public async Task Getreasons()
        {
            var uri = new Uri(string.Format(Settings.Ngrok + "Paramedic/Getorderfaireasons", string.Empty));
            _client = new HttpClient();
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Reasons = JsonConvert.DeserializeObject<List<FailReasons>>(content);
                for(int i = 0; i <= Reasons.Count; i++)
                {
                    failReasons.Add(new FailReasons { Id = Reasons[i].Id, Reason = Reasons[i].Reason });
                }
            }
            else
            {
                DependencyService.Get<Toast>().Show("Faild To View Hospitals");
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] String PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        
    }
}
