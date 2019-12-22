using Ambulance.Paramedic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance.Paramedic.Services
{
    public  class RecordHistoryServices
    {
        public List<RecordHistory> GetRecords()
        {

            var List = new List<RecordHistory>
            {
                new RecordHistory
                {
                    TripId = 125,
                    PatientName = "Amgad",
                    PatientAddress = "52 st de haram"

                }
            };
            return List;

        }
    }
}

