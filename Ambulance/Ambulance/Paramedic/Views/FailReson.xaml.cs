using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Ambulance.Paramedic.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.Paramedic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FailReson : ContentPage
    {
        public FailReson()
        {
            InitializeComponent();
            BindingContext = new FailResonViewmodel();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            HttpClient _client = new HttpClient();

            var details = e.Item as Ambulance.Paramedic.Models.FailReasons;
            HubData data = new HubData();
            data = JsonConvert.DeserializeObject<HubData>(Helper.Settings.permantdata) as HubData;
            OrderConfirmation order = new OrderConfirmation();
            order.OrderStatus = false;
            order.ArrivalTime = DateTime.Now;
            order.OrderFailureReasonId = details.Id;
            order.OrderId = data.OrderId;
            var Json = JsonConvert.SerializeObject(order);
            var uri = new Uri(string.Format(Settings.Ngrok + "Paramedic/ConfirmOrder", string.Empty));
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                DependencyService.Get<Toast>().Show("send succesfully");
                await Task.Delay(1000);
                Settings.permantdata = null;
                await Navigation.PushModalAsync(new ParamedicProfile());
                
            }
            else
            {
                DependencyService.Get<Toast>().Show("Faild to send Request");
            }


        }
    }
}
