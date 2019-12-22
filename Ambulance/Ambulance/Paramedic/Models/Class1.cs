using Ambulance.Model.PatientData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Models
{
    public class HubData
    {

        public string PatientName { get; set; }
        public List<ReportTuble> PatientReports { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Guid OrderId { get; set; }
        public List<Payment> PaymentMethods { get; set; }

        public HubData()
        {
             PatientReports= new List<ReportTuble>();
            PaymentMethods= new List<Payment>();
 
        }

    }

}
