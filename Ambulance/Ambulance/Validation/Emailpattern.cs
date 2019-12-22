using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ambulance.Validation
{
    class Emailpattern
    {
        string emailpattern = "^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\\-+)|([A-Za-z0-9]+\\.+)|([A-Za-z0-9]+\\++))*[A-Za-z0-9_]+@((\\w+\\-+)|(\\w+\\.))*\\w{1,63}\\.[a-zA-Z]{2,6}$";


        public Boolean isverfied(string email)
        {
            if(email != "" || Regex.IsMatch(email, emailpattern))
            return true;

            return false;
        }
    }
}
