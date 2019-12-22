using Ambulance.Helper;
using Ambulance.Model.PatientData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.ViewModel
{
    class RelativeListViewmodel
    {
        public ObservableCollection<PatientRelatives> relatives { get; set; }

        public string name;
        public string number;
        public bool isenabled;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (name == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                if(number.Length<11)
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Number));
            }
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


        public Command<PatientRelatives> Removecommand
        {
            get
            {
                return new Command<PatientRelatives>(async (relative) =>
                {
                    Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Helper.Settings.GeneralSettings);
                    HttpClient _client;
                    _client = new HttpClient();
                    var uri = new Uri(String.Format(Settings.Ngrok + "patient/DeletePatientRelatives/" + relative.ID+"/"+patientdata.ID)); 
                    var response = await _client.DeleteAsync(uri);
                    var data = patientdata.patientRelatives;
                    if (response.IsSuccessStatusCode)
                    {
                        //patientdata.patientRelatives.Equals(relative);
                        patientdata.patientRelatives.Remove(relative);
                        relatives.Remove(relative);//
                        Helper.Settings.GeneralSettings = patientdata.ToString();
                        DependencyService.Get<Toast>().Show(Helper.Settings.GeneralSettings.Equals(relative).ToString());
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Faild to Connect Server");
                    }

                    

                });
            }
        }
        public Command<PatientRelatives> Updatecommand
        {
            get
            {
                return new Command<PatientRelatives>(async (relative) =>
                {
                    String id = relative.ID;
                    Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Helper.Settings.GeneralSettings);
                    HttpClient _client;
                    _client = new HttpClient();
                    Ambulance.Model.PatientRelatives relativedata = new Ambulance.Model.PatientRelatives();
                    relativedata.ID = relative.ID;
                    relativedata.username = Name;
                    relativedata.phoneNumber=Number;
                    var Content = JsonConvert.SerializeObject(relativedata);
                    var content = new StringContent(Content, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync(Settings.Ngrok+"patient/EditPatientRelatives/" + id, content);
                    var data = patientdata.patientRelatives;
                    if (response.IsSuccessStatusCode)
                    {
                        DependencyService.Get<Toast>().Show("Updated Successfully");
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Try Again Later");
                    }
                    
                });
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelativeListViewmodel()
        {
            try { 
            Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;
            relatives = new ObservableCollection<PatientRelatives>();
            var responseObj = patientdata.patientRelatives;
            foreach (PatientRelatives item in responseObj)
            {
                relatives.Add(new PatientRelatives { ID = item.ID, Name = item.Name });
            }
              }
            catch (Exception e)
            {
                DependencyService.Get<Toast>().Show("No internet connection");
            }
            
         }


    }



}