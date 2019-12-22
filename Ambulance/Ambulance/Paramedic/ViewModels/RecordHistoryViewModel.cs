using Ambulance.Paramedic.Models;
using Ambulance.Paramedic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ambulance.Paramedic.ViewModels
{
    
        public class RecordHistoryViewModel : INotifyPropertyChanged
        {
            private List<RecordHistory> _RecordHistoryList;

            public List<RecordHistory> RecordHistoryList
        {
                get { return _RecordHistoryList; }

                set
                {
                _RecordHistoryList = value;
                    OnPropertyChanged();
                }

            }

            public RecordHistoryViewModel()
            {
                var RecordHistoryServices = new RecordHistoryServices();
            RecordHistoryList = RecordHistoryServices.GetRecords();
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] String PropertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }

