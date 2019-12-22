using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Helper
{
    class Location
    {
        public Boolean Locationgps()
        {
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                return false;
            }
            return true;
        }
    }
}
