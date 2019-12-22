using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Ambulance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             MainPage = new MainPage();
            //MainPage = new Ambulance.Paramedic.Views.RequestPage();
            // MainPage = new Ambulance.UserViews.UserViews();
            // MainPage = new Ambulance.UserViews.AddReport();
            // MainPage = new Ambulance.UserViews.Forgetpassword();
            // MainPage = new Ambulance.UserViews.Forgetpassword();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
