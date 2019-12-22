using Ambulance.Helper;
using Ambulance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Ambulance.Model;
using Ambulance.Model.PatientData;
using System.Net.Http;

namespace Ambulance.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserViews : MasterDetailPage
    {

        public List<Masterpageitems> MenuList { get; set; }
      

        Connection con = new Connection();
        Location loc = new Location();
        public UserViews ()
		{
			InitializeComponent ();

            MenuList = new List<Masterpageitems>();
            BindingContext = new RequestViewmodel();

            MenuList.Add(new Masterpageitems() { Title = "Home", Icon = "home.png", TargetType = typeof(Page1) });
            MenuList.Add(new Masterpageitems() { Title = "Setting", Icon = "setting.png", TargetType = typeof(Userprofile) });
            MenuList.Add(new Masterpageitems() { Title = "Requests", Icon = "Request.png", TargetType = typeof(RequestesList) });
            MenuList.Add(new Masterpageitems() { Title = "History", Icon = "help.png", TargetType = typeof(Patient_history) });
            MenuList.Add(new Masterpageitems() { Title = "Relatives", Icon = "relatives.png", TargetType = typeof(RelativesList) });
            MenuList.Add(new Masterpageitems() { Title = "LogOut", Icon = "logout.png", TargetType = null });


            navigationdrawablelist.ItemsSource = MenuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Page1)));
        }


        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Masterpageitems)e.SelectedItem;
            
            if (item.TargetType==null)
            {
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var Removedpage in existingPages)
                {
                    Navigation.RemovePage(Removedpage);
                }
                Helper.Settings.GeneralSettings = null;
                Navigation.PopModalAsync();
                Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                Type page = item.TargetType;
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                IsPresented = false;
            }
        }
    }
}
