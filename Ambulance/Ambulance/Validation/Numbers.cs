using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.Validation
{
    class Numbers : Behavior<Entry>
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
            int Test;
            bool isvaild = int.TryParse(e.NewTextValue, out Test);
            ((Entry)sender).TextColor = isvaild ? Color.Black : Color.Red;
        }
    }
}
