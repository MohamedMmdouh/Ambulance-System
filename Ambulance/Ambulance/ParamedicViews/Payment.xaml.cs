using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace Ambulance.ParamedicViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentPage
    {

        public Payment()
        {
            InitializeComponent();

             anspicker.ItemsSource = new[]
                {
                    "Cash",
                    "On Elctricity bill"
                };

            anspicker.CheckedChanged += ansPicker_CheckedChanged;
         }

         private void ansPicker_CheckedChanged(object sender, int e)
         {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1)
                {
                    return;
                }

                DisplayAlert("Info", radio.Text, "OK");
         }
    }
}