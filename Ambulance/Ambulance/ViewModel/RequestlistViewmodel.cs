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
using System.Collections.ObjectModel;

namespace Ambulance.ViewModel
{
    class RequestlistViewmodel
    {
        public ObservableCollection<Model.PatientData.PatientRequests> reportdata { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RequestlistViewmodel()
        {
            try
            {
                Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;
                reportdata = new ObservableCollection<Model.PatientData.PatientRequests>();
                var responseObj = patientdata.patientrequests;
                foreach (Model.PatientData.PatientRequests item in responseObj)
                {
                    reportdata.Add(new Model.PatientData.PatientRequests { ID = item.ID, CreationTime = item.CreationTime });
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<Toast>().Show(ex.ToString());
            }


        }
    }
}



