using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.Validation
{
    class Phone : Behavior<Entry>
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
        private void Numberchange(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                // If the new value is longer than the old value, the user is
                if (args.OldTextValue != null && args.NewTextValue.Length < args.OldTextValue.Length)
                    return;

                var value = args.NewTextValue;

                if (value.Length < 11)
                {
                    ((Entry)sender).TextColor = Color.Red;
                }
                else
                {
                    ((Entry)sender).TextColor = Color.Black;
                }


               ((Entry)sender).Text = args.NewTextValue;
            }
        }
    }
}