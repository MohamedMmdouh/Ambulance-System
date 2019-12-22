using Ambulance.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Loginviewmodel();

            Signup();
            Forgetpass();

        }

        private void Signup()
        {
            SignUp.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushModalAsync(new Ambulance.UserViews.Registeration());

                })
            });
        }
   /*     protected override void OnAppearing()
        {
            var existingPages = Application.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var Removedpage in existingPages)
            {
                Navigation.RemovePage(Removedpage);
            }
        }
    */
        private void Forgetpass()
        {
            Forgetpassword.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushModalAsync(new Ambulance.UserViews.Forgetpassword());
                })
            });
        }
    }
}
