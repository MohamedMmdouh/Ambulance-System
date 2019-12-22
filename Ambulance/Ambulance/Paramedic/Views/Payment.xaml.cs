using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace Ambulance.Paramedic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentPage
    {

        double latitude;
        double longitude;
        Guid hospitalid;
        String paymenttype;

        public Payment(Hospitals hospitasl)
        {
            latitude = hospitasl.Latitude;
            longitude = hospitasl.Longitude;
            hospitalid = hospitasl.HospitalId;
            InitializeComponent();
            ansPicker.ItemsSource = new[]
            {
                " Cash",
                " Bill",
                " Acciedent"
            };

            ansPicker.CheckedChanged += ansPicker_CheckedChanged;
        }

        private void ansPicker_CheckedChanged(object sender, int e)
        {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1)
            {
                return;
            }
            paymenttype = radio.Text;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try { 
            HttpClient _client = new HttpClient();
            var radio = sender as CustomRadioButton;
            OrderConfirmation order = new OrderConfirmation();
            ReportTuble report = new ReportTuble();
            if (paymenttype != null)
            {
                var text = paymenttype;
                HubData data = new HubData();

                data = JsonConvert.DeserializeObject<HubData>(Helper.Settings.permantdata) as HubData;
                
                var payments = data.PaymentMethods;
                Ambulance.Paramedic.Models.Payment pay = new Ambulance.Paramedic.Models.Payment();
                foreach (Ambulance.Paramedic.Models.Payment item in payments)
                {
                    if (item.Type == paymenttype)
                    {
                        order.PaymentId = item.Id;
                    }
                }
                if (order.PaymentId != null)
                {
                    order.OrderStatus = true;
                    order.HospitalId = hospitalid;
                    order.OrderId = data.OrderId;
                    order.ArrivalTime = DateTime.Now;
                    var Json = JsonConvert.SerializeObject(order);
                    //ConfirmOrder    
                    var uri = new Uri(string.Format(Settings.Ngrok+"Paramedic/ConfirmOrder", string.Empty));
                    var content = new StringContent(Json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response;
                    response = await _client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        DependencyService.Get<Toast>().Show("send succesfully");
                        await Navigation.PushModalAsync(new ParamedicProfile());
                        //await Navigation.PushModalAsync(new MainPage());
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Faild to send Request");
                    }


                }
                else
                {
                    DependencyService.Get<Toast>().Show("Invalid Payment Method");
                }

            }
            else
            {
               await DisplayAlert("Hint", "Payment must be selected", "OK");
            }
            }
            catch(Exception exception)
            {
                DependencyService.Get<Toast>().Show("No Internet connection");
            }
        } 
        private async void Navigate_Clicked(object sender, EventArgs e)
        {
            await Map.OpenAsync(latitude, latitude, new MapLaunchOptions
            {
                Name = "space needle",
                NavigationMode = NavigationMode.Driving
            });
        }
        protected override void OnDisappearing()
        {
            Navigation.PopModalAsync();
        }


    }
}