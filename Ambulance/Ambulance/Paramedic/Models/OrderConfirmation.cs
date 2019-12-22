using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Models
{
    public class OrderConfirmation
    {
        public Boolean OrderStatus { get; set; }
        public Guid OrderFailureReasonId { get; set; }

        public Guid HospitalId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Guid OrderId { get; set; }

    }
}