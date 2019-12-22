using Ambulance.Helper;
using Ambulance.Model;
using Ambulance.Paramedic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.Paramedic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterCarNo : ContentPage
    {
        string carnum = string.Empty;
        public EnterCarNo()
        {
            InitializeComponent();
        }
        protected override  void OnAppearing()
        {
            Paramedicdata paramedicdata = JsonConvert.DeserializeObject<Paramedicdata>(Settings.GeneralSettings) as Paramedicdata;
            DependencyService.Get<Toast>().Show("Hello "+paramedicdata.UserData.Username);
        }
        protected override void OnDisappearing()
        {
            Navigation.PopModalAsync();
        }


    }
}