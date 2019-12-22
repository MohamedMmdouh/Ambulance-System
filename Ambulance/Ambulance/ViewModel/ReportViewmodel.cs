using Ambulance.Helper;
using Ambulance.Model;
using Ambulance.Model.PatientData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.ViewModel
{
    class ReportViewmodel : INotifyPropertyChanged
    {
        public bool isenabled;
        public ObservableCollection<Reportdata> reportdata { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Isenabled
        {
            get
            {
                return isenabled;
            }
            set
            {
                isenabled = value;
                OnPropertyChanged(nameof(Isenabled));
            }
        }
        public ObservableCollection<Reportdata> Reportsdata
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

        public ReportViewmodel()
        {
            try
            {
                Patientdata patientdata = JsonConvert.DeserializeObject<Patientdata>(Settings.GeneralSettings) as Patientdata;
                if (reportdata == null)
                {
                    reportdata = new ObservableCollection<Reportdata>();
                    var responseObj = patientdata.reports;
                    foreach (Reports item in responseObj)
                    {
                        reportdata.Add(new Reportdata { ID = item.ID, Name = item.hospitalData.Name });
                    }
                }
                else
                {
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

