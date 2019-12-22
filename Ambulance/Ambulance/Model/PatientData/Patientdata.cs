using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Model.PatientData
{
    public class Patientdata
    {
        public String ID { get; set; }
        public User user { get; set; }
        public String Birthdate { get; set; }
        public String BloodType { get; set; }
        public String history { get; set; }
        public IEnumerable<Reports> reports { get; set; }
        public List<PatientRelatives> patientRelatives { get; set; }
        public List<PatientRequests> patientrequests { get; set; }
    }
}
