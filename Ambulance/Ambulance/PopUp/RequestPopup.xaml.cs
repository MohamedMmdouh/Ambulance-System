using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambulance.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestPopup : PopupPage
	{
		public RequestPopup ()
		{
			InitializeComponent ();
		}
	}
}