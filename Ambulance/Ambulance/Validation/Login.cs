using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.Validation
{
    class Login : Behavior<Entry>
    {


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += BindableOnTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= BindableOnTextChanged;

        }
        private void BindableOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var password = e.NewTextValue;

            var passwordentry = sender as Entry;

            if (password.Length >= 8)
            {
                passwordentry.TextColor = Color.Black;
            }
            else
            {
                passwordentry.TextColor = Color.Red;
            }
        }
    }
}