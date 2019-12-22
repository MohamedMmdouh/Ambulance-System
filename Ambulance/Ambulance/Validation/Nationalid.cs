using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.Validation
{
    class Nationalid : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Numberchange;

            base.OnAttachedTo(bindable);
        }



        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Numberchange;

        }
        private void Numberchange(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 14)
            {
                ((Entry)sender).TextColor = Color.Red;
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
            }
        }
    }
}
