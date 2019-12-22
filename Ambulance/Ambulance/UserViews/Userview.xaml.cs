using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambulance.Helper;
using Ambulance.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ambulance.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace Ambulance.UserViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Userview : MasterDetailPage    
	{
        Connection con = new Connection();
        Location loc = new Location();
        User user = new User();
        public Userview ()
		{
            InitializeComponent();

            BindingContext = new RequestViewmodel();

          //  User user = JsonConvert.DeserializeObject<User>(Helper.Settings.GeneralSettings) as User;
        //    DependencyService.Get<Toast>().Show("Welcome "+user.Username+" !");
        }

        protected override async void OnAppearing()
        {
            await Task.Delay(2000);
            HubConnection connection = new HubConnectionBuilder()
                    .WithUrl("https://ambulancesystem20190520065128.azurewebsites.net/RequestHub")
                    .Build();

            connection.On<string>("ReceiveMessage", (message) =>
            {
                var mes = message;
                DependencyService.Get<Toast>().Show(mes);
            });

            await connection.StartAsync();//.GetAwaiter().GetResult();
            DependencyService.Get<Toast>().Show(connection.State.ToString());

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
    }
}