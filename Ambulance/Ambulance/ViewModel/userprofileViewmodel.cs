using Ambulance.Helper;
using Ambulance.Model.PatientData;
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
    class user_profile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string userprofile = string.Empty;
        string username = string.Empty;
        string email = string.Empty;
        string birthdate = string.Empty;
        string phonenumber = string.Empty;
        string bloodtype = string.Empty;
        string history = string.Empty;
        string nationalid = string.Empty;
        string newpassword = string.Empty;

        bool isenabled = false;

        private void OnPropertyChanged([CallerMemberName] string C = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(C));
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
                OnPropertyChanged(nameof(Email));
            }
        }
        public string History
        {
            get
            {
                return history;
            }
            set
            {
                history = value;
                if (history == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(History));
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                if (username == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                birthdate = value;
                if (birthdate == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(Birthdate));
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phonenumber;
            }
            set
            {
                phonenumber = value;
                if (phonenumber == "" || phonenumber.ToString().Length < 11)
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string BloodType
        {
            get
            {
                return bloodtype;
            }
            set
            {
                bloodtype = value;
                if (bloodtype == "")
                {
                    Isenabled = false;
                }
                else
                {
                    Isenabled = true;
                }
                OnPropertyChanged(nameof(BloodType));
            }
        }
        public string NationalID
        {
            get
            {
                return nationalid;
            }
            set
            {
                nationalid = value;
                OnPropertyChanged(nameof(NationalID));
            }
        }
        public string NewPassword
        {
            get
            {
                return newpassword;
            }
            set
            {
                newpassword = value;


                OnPropertyChanged(nameof(NewPassword));
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
        public user_profile()
        {
            Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;
            if (patientdata.user.ImageURl == null)
            {
                this.userprofile = "profile.png";
            }
            else
            {
                this.userprofile = patientdata.user.ImageURl;
            }
            this.Username = patientdata.user.Username;
            this.Email = patientdata.user.Email;
            this.Birthdate = patientdata.Birthdate;
            this.PhoneNumber = patientdata.user.phonenumber;
            this.NationalID = patientdata.user.NationalID;
            this.BloodType = patientdata.BloodType;
            this.history = patientdata.history;
            updateprofile = new Command(async () => await update());
        }
        public Command updatepassword { get; }
        public Command updateprofile { get; }



        public async Task update()
        {
            Isenabled = false;
            Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;

            var client = new HttpClient();
            profile Profile = new profile();
            Profile.Id = patientdata.ID;
            Profile.Email = Email;
            Profile.Username = Username;
            Profile.NationalId = NationalID;
            Profile.PhoneNumber = PhoneNumber;
            Profile.History = History;
            Profile.BloodType = BloodType;
            Profile.Birthdate = Birthdate;

            String Userdata = JsonConvert.SerializeObject(Profile);
            var content = new StringContent(Userdata, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(Helper.Settings.Ngrok+"Patient/UpdatePatientProfile", content);

            if (response.IsSuccessStatusCode)
            {
     
                DependencyService.Get<Toast>().Show("Data updated successfully");
            }
            else
            {
                DependencyService.Get<Toast>().Show("Check Data Again");
            }
        }

    }
}
