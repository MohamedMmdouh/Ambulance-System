using Ambulance.Helper;
using Ambulance.Paramedic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Ambulance.Paramedic.ViewModels
{
    class ReportsViewmodel : INotifyPropertyChanged
    {
        public ObservableCollection<ReportTuble> reportdata { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
        public ObservableCollection<ReportTuble> Reportsdata
        {
            get
            {
                return Reportsdata;
            }
            set
            {
                Reportsdata = value;
                OnPropertyChanged(nameof(Reportsdata));

            }
        }

        public ReportsViewmodel()
        {
            try
            {
                HubData requestdata = JsonConvert.DeserializeObject<HubData>(Settings.permantdata) as HubData;
                if (requestdata.PatientReports!=null)
                {
                    reportdata = new ObservableCollection<ReportTuble>();
                    var responseObj = requestdata.PatientReports;
                    foreach (ReportTuble item in responseObj)
                    {
                        reportdata.Add(new ReportTuble { HospitalName = item.HospitalName, DiseaseName = item.DiseaseName});
                    }
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Patient doesn't have any reports");
                    reportdata.Clear();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<Toast>().Show(ex.ToString());
            }


        }
    }
}
