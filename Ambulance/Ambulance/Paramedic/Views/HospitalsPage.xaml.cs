using Ambulance.Paramedic.Models;
using Ambulance.Paramedic.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.Paramedic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HospitalsPage : ContentPage
	{
        HubConnection connection;
        public HospitalsPage(HubConnection connection)
		{
            this.connection = connection;

			InitializeComponent ();
            BindingContext = new HospitalViewModel();

        }

        private  void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {          
                var details = e.Item as Hospitals;
                HubData hub = JsonConvert.DeserializeObject<HubData>(Helper.Settings.permantdata);
                Paramedicdata paramedic= JsonConvert.DeserializeObject<Paramedicdata>(Helper.Settings.GeneralSettings);
                SendParamedicHospitalChoice(details.HospitalId,hub.OrderId,paramedic.Id);
                Navigation.PushModalAsync(new Ambulance.Paramedic.Views.Payment(details));
            }
            catch(Exception exception)
            {
                DependencyService.Get<Toast>().Show(exception.Message);
            }
        }

        private void Fail_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FailReson());
        }
        async void SendParamedicHospitalChoice(Guid hospitalId,Guid orderId,Guid paramedicId)
        {
            try
            {
                await connection.InvokeAsync("ReceiveParamedicHospitalChoice",
                                            arg1: hospitalId,
                                            arg2: orderId,
                                            arg3: paramedicId);
            }
            catch (Exception ex)
            {
                throw new Exception("Faile - " + ex.Message);
            }
        }
    }
}
