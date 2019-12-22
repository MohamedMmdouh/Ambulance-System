using Ambulance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.ViewModel
{
    class RelativeupdateViewmodel : INotifyPropertyChanged
    {
        string relativename = string.Empty;
        string relativenumber = string.Empty;
        bool isenabled = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Relativename
        {
            get
            {
                return relativename;
            }
            set
            {
                relativename = value;
                if (relativename == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(Relativename);
            }
        }
        public String Relativenumber
        {
            get
            {
                return relativenumber ;
            }
            set
            {
                relativenumber = value;
                if (relativenumber == "" || relativenumber.Length < 11)
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(Relativenumber);
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
                OnPropertyChanged(Relativenumber);
            }
        }
        /* public Command<PatientRelatives> EditComand
         {
             get
             {
                 return new Command<PatientRelatives>(async (relative) =>
                 {
                     //Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Helper.Settings.GeneralSettings);
                     HttpClient _client;
                     _client = new HttpClient();
                     var uri = new Uri(String.Format("http://0db7f8f6.ngrok.io/api/patient/DeletePatientRelatives/" + relative.ID + "/" + patientdata.ID));
                     var response = await _client.DeleteAsync(uri);
                     var data = patientdata.patientRelatives;
                     if (response.IsSuccessStatusCode)
                     {
                         patientdata.patientRelatives.Equals(relative);

                         relatives.Remove(relative);
                         DependencyService.Get<Toast>().Show(Helper.Settings.GeneralSettings.Equals(relative).ToString());
                     }
                     else
                     {
                         DependencyService.Get<Toast>().Show("Faild to Connect Server");
                     }



                 });
             }
         }*/
    }
}
