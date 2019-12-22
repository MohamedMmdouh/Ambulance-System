using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Ambulance.Helper
{
   
    class Connection
    {
        public string ngrok= "http://aa48e2f8.ngrok.io";
        public Boolean Internet()
        {
          if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                   return false;
                }
                  return true;
        }   
    }
}
