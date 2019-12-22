using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ambulance.Model
{
    class User
    {
       
        public String ID { get; set; }
       
        public String Email { get; set; }

        public String Username { get; set; }

       
        public String Password { get; set; }

       
        public String NationalID { get; set; }

       
        public String imageUrl { get; set; }


        public DateTime Birthdate { get; set; }

        public String BloodType { get; set; }

        public String PhoneNumber { get; set; }

        
    }

}