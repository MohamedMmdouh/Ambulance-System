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
    class RequestViewmodelcs : INotifyPropertyChanged
    {
        HubConnection connection;
        bool isconnected = false;
        string request = string.Empty;
        public Command ReciveNotification { get; }
        public Command Connectcommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Isconnected
        {
            get
            {
                return isconnected;
            }
            set
            {
                isconnected = value;
                OnPropertyChanged(nameof(Isconnected));
            }
        }
        public String Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
                if (connection.State.ToString()=="Disconnected")
                {
                    Request = "Disconnect";
                    request = "Disconnect";
                    Isconnected = false;
                }
                else
                {
                    Request = "Connected";
                    request = "Disconnect";
                    Isconnected = true;
                }
                OnPropertyChanged(nameof(Request));
            }
        }

        public RequestViewmodelcs() 
        {

            Paramedicdata paramedicdata = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;
  

  
             connection = new HubConnectionBuilder()
             .WithUrl(Settings.NgrokHub)
             .Build();

            connection.StartAsync();
            var x = connection.State;

            connection.Closed += async (error) =>
            {
                await Task.Delay(3000);
                await Connect();
            };

            connection.On<string>("ConnectionReceived",  (message)=>
            {
                DependencyService.Get<Toast>().Show(message.ToString());
                var paramedicId = paramedicdata.Id; //Change This to the suitable paramedic who choosen
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

                Request = "It 's seem you have anew Request";
                Task.Delay(2000);
                //Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new Ambulance.Paramedic.Views.NavigationPage(hub));
            });

        }

        async Task Connect()
        {
            await connection.StartAsync();

        }
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
 
    }
}
