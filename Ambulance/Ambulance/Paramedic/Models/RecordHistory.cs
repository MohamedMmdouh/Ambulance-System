using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Models
{
  public  class RecordHistory
    {
        public int TripId { get; set; }

        public string PatientName { get; set; }

        public string PatientAddress { get; set; }
    }
}
