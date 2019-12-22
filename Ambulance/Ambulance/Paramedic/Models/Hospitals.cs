using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Models
{
    public class Hospitals
    {
        public Guid HospitalId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string Specialization { get; set; }


    }
}
