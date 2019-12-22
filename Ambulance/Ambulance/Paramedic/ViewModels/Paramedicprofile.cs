using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.Paramedic.ViewModels
{
    class Paramedicprofile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        HubConnection connection;

        string userprofile = string.Empty;
        string username = string.Empty;
        string email = string.Empty;
        string phonenumber = string.Empty;
        string nationalid = string.Empty;

        bool isenabled = false;
        bool isvisible = false;
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
        public string UserProfile
        {
            get
            {
                return userprofile;
            }
            set
            {
                userprofile = value;
                OnPropertyChanged(nameof(UserProfile));
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
        public bool Isvisible
        {
            get
            {
                return isvisible;
            }
            set
            {
                isvisible = value;
                OnPropertyChanged(nameof(Isvisible));
            }
        }


        public Paramedicprofile()
        {
            Paramedicdata paramedic = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;
            if (paramedic.UserData.ImageUrl == null)
            {
                this.userprofile = "profile.png";
            }
            else
            {
                this.userprofile = paramedic.UserData.ImageUrl;
            }
            this.Username = paramedic.UserData.Username;
            this.Email = paramedic.UserData.Email;
            this.PhoneNumber = paramedic.UserData.PhoneNumber;
            this.NationalID = paramedic.UserData.NationalId;
            try {
                connection = new HubConnectionBuilder()
                .WithUrl(Settings.NgrokHub)
                .Build();

                connection.StartAsync();
                //         Task.Run(async () => { await Connect(); }).GetAwaiter();
                var x = connection.State;

                connection.Closed += async (error) =>
                {
                    await Task.Delay(3000);
                    await connection.StartAsync();
                };

                connection.On<string>("ConnectionReceived", (message) =>
                {
                    DependencyService.Get<Toast>().Show(message.ToString());
                    var paramedicId = paramedic.Id; //Change This to the suitable paramedic who choosen
                    AuthorizeParamedic(paramedicId);
                });

                connection.On<object>("ConnectionStatus", (message) =>
                {
                    CrossLocalNotifications.Current.Show("Message", message.ToString(), 101, DateTime.Now.AddSeconds(2));

                });
                async void AuthorizeParamedic(Guid paramedicId)
                {
                    try
                    {
                        await connection.InvokeAsync("AuthorizeParamedic", arg1: paramedicId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Faile to get this paramedic - " + ex.Message);
                    }
                }

                connection.On<Object>("ReceiveOrder", (test) =>
                {
                    var data = test;
                    HubData hub = JsonConvert.DeserializeObject<HubData>(test.ToString());
                    Settings.permantdata = data.ToString();
                    CrossLocalNotifications.Current.Show("Message", "You have new request", 101, DateTime.Now.AddSeconds(2));
                    Isvisible = true;
                });
                Navigate = new Command(() => Move(connection));
            }
            catch (Exception e)
            {
                DependencyService.Get<Toast>().Show(e.ToString());
            }
        }
        public Command Navigate { get; }

        public  void Move(HubConnection connection)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.Paramedic.Views.NavigationPage(connection));
        }

    }
}
