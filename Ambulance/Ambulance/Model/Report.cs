using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ambulance.Model
{
    class Report
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid PatientId { get; set; }
        public Guid HospitalId { get; set; }
        public string DiseaseName { get; set; }
        public string Description { get; set; }
        public bool IsChronicDisease { get; set; }
    }
}
