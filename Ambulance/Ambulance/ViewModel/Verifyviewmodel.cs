using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ambulance.ViewModel 
{
    class Verifyviewmodel : INotifyPropertyChanged
    {
        String code = string.Empty;
        bool isenabled = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                if (code.Length<4)
                {
                    Isenabled = false;
                }
                else
                {
                    isenabled = true;
                }
                OnPropertyChanged(nameof(Code));
            }
        }
        public Boolean Isenabled
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
        public Verifyviewmodel()
        {
            verifycommand = new Command(async () => await Verify());
        }
        public Command verifycommand { get; }

        public async Task Verify()
        {
            //verify account 
            Isenabled = false;
            var client = new HttpClient();

        }
    }
}
