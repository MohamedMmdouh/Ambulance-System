using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Models
{
    public class Paramedicdata
    {
        public Guid Id { get; set; }
        public UserData UserData { get; set; }
        public AmbulanceCarData AmbulanceCarData { get; set; }
    }
    public class UserData
    {

        public string Username { get; set; }

        public string Email { get; set; }


        public string PhoneNumber { get; set; }


        public string NationalId { get; set; }


        public string ImageUrl { get; set; }
    }
    public class AmbulanceCarData
    {
        public Guid Id { get; set; }
        public string CarNumber { get; set; }
    }
}
