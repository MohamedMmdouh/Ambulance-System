using Ambulance.Helper;
using Ambulance.Model.PatientData;
using Ambulance.PopUp;
using Ambulance.ViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelativesList : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public RelativesList()
        {
            InitializeComponent();
            BindingContext = new RelativeListViewmodel();
        }

         void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as PatientRelatives;
            Settings.permantdata = details.ID;
            //Updatedata.IsVisible = true;
             Navigation.PushModalAsync(new ViewRelative());
        }

        private void AddRelative_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddRelatives());
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {

            var button = sender as Button;
            var data= button?.BindingContext as PatientRelatives;
            var item = BindingContext as RelativeListViewmodel;
            item?.Removecommand.Execute(data);

        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var data = button?.BindingContext as PatientRelatives;
            var item = BindingContext as RelativeListViewmodel;
            item?.Updatecommand.Execute(data);
        }
    }
}
